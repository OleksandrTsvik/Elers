namespace Domain.Shared;

public class ValidationError
{
    public string Code { get; }
    public string Message { get; }

    public ValidationError(string code, string message)
    {
        Code = code;
        Message = message;
    }
}
