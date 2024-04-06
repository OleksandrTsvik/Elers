namespace Application.Auth.DTOs;

public class AuthUserDto
{
    public string Email { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
    public List<string> Roles { get; set; } = new();
    public List<string> Permissions { get; set; } = new();
}
