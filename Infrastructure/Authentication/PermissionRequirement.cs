using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authentication;

public class PermissionRequirement : IAuthorizationRequirement
{
    public string[] Permissions { get; }

    public PermissionRequirement(string[] permissions)
    {
        Permissions = permissions;
    }
}
