using Domain.Enums;

namespace Application.Auth.DTOs;

public class AuthUserDto
{
    public required UserType Type { get; init; }
    public required string Email { get; init; }
    public required string? AvatarUrl { get; init; }
    public required List<PermissionType> Permissions { get; init; }
}
