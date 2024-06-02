using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.CourseMemberPermissions;

public class CourseMemberPermissionRequirement : IAuthorizationRequirement
{
    public string[] CourseMemberPermissions { get; }
    public string[] UserPermissions { get; }

    public CourseMemberPermissionRequirement(string[] courseMemberPermissions, string[] userPermissions)
    {
        CourseMemberPermissions = courseMemberPermissions;
        UserPermissions = userPermissions;
    }
}
