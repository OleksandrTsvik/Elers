namespace Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public DateTime RegistrationDate { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Patronymic { get; set; }
    public string? AvatarUrl { get; set; }
    public DateTime? BirthDate { get; set; }

    public List<Role> Roles { get; set; } = new();

    public string FullName => $"{LastName} {FirstName} {Patronymic}";

    public User()
    {
        RegistrationDate = DateTime.UtcNow;
    }
}
