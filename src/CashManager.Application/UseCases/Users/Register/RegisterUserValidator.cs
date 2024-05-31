using CashManager.Application.Interfaces;
using CashManager.Communication.Requests;
using FluentValidation;
using FluentValidation.Results;

namespace CashManager.Application.UseCases.Users.Register;

public class RegisterUserValidator(IResourceManagerProvider resourceManagerProvider)
    : AbstractValidator<RequestRegisterUser>
{
    private readonly IResourceManagerProvider _resourceManagerProvider = resourceManagerProvider;

    public override ValidationResult Validate(ValidationContext<RequestRegisterUser> context)
    {
        var resourceManager = _resourceManagerProvider.GetResourceManager();

        RuleFor(user => user.Name)
            .NotEmpty().WithMessage(resourceManager.GetString("NameRequired"));

        RuleFor(user => user.Email)
            .NotEmpty().WithMessage(resourceManager.GetString("EmailRequired"))
            .Must(BeAValidEmail).WithMessage(resourceManager.GetString("EmailInvalid"));

        RuleFor(user => user.Password)
            .NotEmpty().WithMessage(resourceManager.GetString("PasswordRequired"));

        return base.Validate(context);
    }

    private bool BeAValidEmail(string email) => !string.IsNullOrWhiteSpace(email);
}
