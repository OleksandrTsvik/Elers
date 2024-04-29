namespace Application.Users.GetListUsers;

public class GetListUserItemResponse
{
    public Guid Id { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Patronymic { get; init; }
    public string Email { get; init; } = string.Empty;
    public string[] Roles { get; init; } = [];
}
