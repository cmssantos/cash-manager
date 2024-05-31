namespace CashManager.Exception.ExceptionsBase;

public class ErrorOnValidationException(List<string> errors) : CashManagerException
{
    public List<string> Errors { get; set; } = errors;
}
