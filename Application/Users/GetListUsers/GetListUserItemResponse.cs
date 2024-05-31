namespace Application.Users.GetListUsers;

public class GetListUserItemResponse
{
    public required Guid Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Patronymic { get; init; }
    public required string Email { get; init; }
    public required string[] Roles { get; init; }
}
