using CashManager.Communication.Requests;

namespace CashManager.Application.UseCases.Users.Register;

public interface IRegisterUserUseCase
{
    Task Execute(RequestRegisterUser request);
}