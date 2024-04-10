namespace Domain.Shared;

public class ExceptionResponse
{
    public int StatusCode { get; }
    public string Message { get; }
    public object? Details { get; }

    public ExceptionResponse(int statusCode, string message, object? details)
    {
        StatusCode = statusCode;
        Message = message;
        Details = details;
    }
}
