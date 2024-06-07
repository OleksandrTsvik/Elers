namespace Application.Users.DTOs;

public class UserDto
{
    public required Guid Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Patronymic { get; init; }
    public required string? AvatarUrl { get; init; }
}
