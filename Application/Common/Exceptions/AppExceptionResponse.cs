namespace Application.Common.Exceptions;

public class AppExceptionResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string? Details { get; set; }

    public AppExceptionResponse(int statusCode, string message, string? details = null)
    {
        StatusCode = statusCode;
        Message = message;
        Details = details;
    }
}
