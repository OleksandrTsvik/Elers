namespace Application.Auth.GetInfo;

public class InfoResponse
{
    public string Email { get; set; } = string.Empty;
    public DateTime RegistrationDate { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Patronymic { get; set; }
    public string? AvatarUrl { get; set; }
    public DateTime? BirthDate { get; set; }
}
