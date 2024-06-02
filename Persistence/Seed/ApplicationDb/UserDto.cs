using Domain.Enums;

namespace Persistence.Seed.ApplicationDb;

public class UserDto
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Patronymic { get; init; }
    public required List<DefaultRole> DefaultRoles { get; init; }
}
