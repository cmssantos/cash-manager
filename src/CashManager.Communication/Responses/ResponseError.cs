namespace CashManager.Communication.Responses;

public class ResponseError
{
    public List<string> ErrorMessages { get; set; }

    public ResponseError(string errorMessage) => ErrorMessages = [errorMessage];

    public ResponseError(List<string> errorMessages) => ErrorMessages = errorMessages;
}
