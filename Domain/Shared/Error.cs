namespace Domain.Shared;

public class Error
{
    public static readonly Error None = new(string.Empty, string.Empty);

    public string Code { get; }
    public string Description { get; }
    public ErrorType Type { get; }

    private Error(string code, string description, ErrorType type = ErrorType.Failure)
    {
        Code = code;
        Description = description;
        Type = type;
    }

    public static Error Create(ErrorType type, string code, string description) =>
        new(code, description, type);
}
