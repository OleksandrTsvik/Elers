using Infrastructure.CourseMemberPermissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Infrastructure.Authentication;

public class PermissionAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
{
    public PermissionAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
        : base(options)
    {
    }

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        AuthorizationPolicy? policy = await base.GetPolicyAsync(policyName);

        bool isPermission = policyName.StartsWith(HasPermissionAttribute.PolicyPrefix);

        bool isCourseMemberPermission = policyName
            .StartsWith(HasCourseMemberPermissionAttribute.CourseMemberPolicyPrefix);

        if (!isPermission && !isCourseMemberPermission)
        {
            return policy;
        }

        if (policy is not null)
        {
            return policy;
        }

        if (isPermission)
        {
            return BuildPermissionPolicy(policyName);
        }
        else if (isCourseMemberPermission)
        {
            return BuildCourseMemberPermissionPolicy(policyName);
        }

        return null;
    }

    private static AuthorizationPolicy BuildPermissionPolicy(string policyName)
    {
        string[] permissions = policyName
            .Substring(HasPermissionAttribute.PolicyPrefix.Length)
            .Split(",");

        return new AuthorizationPolicyBuilder()
            .AddRequirements(new PermissionRequirement(permissions))
            .Build();
    }

    private static AuthorizationPolicy BuildCourseMemberPermissionPolicy(string policyName)
    {
        string[] permissions = policyName.Split(HasCourseMemberPermissionAttribute.Separator);

        string[] courseMemberPermissions = permissions[0]
            .Substring(HasCourseMemberPermissionAttribute.CourseMemberPolicyPrefix.Length)
            .Split(",");

        string[] userPermissions = permissions[1]
            .Substring(HasCourseMemberPermissionAttribute.UserPolicyPrefix.Length)
            .Split(",");

        var requirements = new CourseMemberPermissionRequirement(courseMemberPermissions, userPermissions);

        return new AuthorizationPolicyBuilder()
            .AddRequirements(requirements)
            .Build();
    }
}
