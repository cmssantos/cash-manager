namespace CashManager.Exception.ExceptionsBase;

public class UserAlreadyExistsException(string email) : CashManagerException
{
    public override string Message { get; } = $"User with email {email} already exists.";
}
