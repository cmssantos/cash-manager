using CashManager.Communication.Requests;
using CashManager.Domain.Entities;
using CashManager.Exception.ExceptionsBase;
using FlexRepo.Interfaces;
using Microsoft.Extensions.Logging;

namespace CashManager.Application.UseCases.Users.Register;

public class RegisterUserUseCase(
    IRepository<User, Guid> repository,
    ILogger<RegisterUserUseCase> logger,
    RegisterUserValidator validator)
    : IRegisterUserUseCase
{
    private readonly IRepository<User, Guid> _repository = repository;
    private readonly ILogger<RegisterUserUseCase> _logger = logger;
    private readonly RegisterUserValidator _validator = validator;

    public async Task Execute(RequestRegisterUser request)
    {
        var result = _validator.Validate(request);
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }

        await ExecuteAsync(request);
    }

    private async Task ExecuteAsync(RequestRegisterUser request)
    {
        var existingUser = await _repository.GetSingleOrDefaultAsync(f => f.Email == request.Email);
        if (existingUser is not null)
        {
            _logger.LogWarning("User with email {email} already exists.", request.Email);
            throw new UserAlreadyExistsException(request.Email);
        }

        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            CurrentPassword = request.Password
        };

        await _repository.AddAsync(user, true);
    }
}
