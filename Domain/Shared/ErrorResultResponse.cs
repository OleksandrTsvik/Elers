namespace Domain.Shared;

public class ErrorResultResponse
{
    public string Code { get; }
    public string Message { get; }

    public ErrorResultResponse(string code, string message)
    {
        Code = code;
        Message = message;
    }
}
