using Domain.Enums;

namespace Persistence.Seed.ApplicationDb;

public class UserDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public List<DefaultRole> DefaultRoles { get; set; } = [];
}
