using Domain.Enums;

namespace Application.Auth.DTOs;

public class AuthUserDto
{
    public string Email { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
    public List<PermissionType> Permissions { get; set; } = [];
}
