using Domain.Primitives;

namespace Domain.Entities;

public class User : Entity
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public DateTime RegistrationDate { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Patronymic { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
    public DateTime? BirthDate { get; set; }

    public List<Role> Roles { get; set; } = [];

    public User()
    {
        RegistrationDate = DateTime.UtcNow;
    }
}
