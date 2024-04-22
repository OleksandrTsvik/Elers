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

        if (!policyName.StartsWith(HasPermissionAttribute.PolicyPrefix))
        {
            return policy;
        }

        if (policy is not null)
        {
            return policy;
        }

        string[] permissions = policyName
            .Substring(HasPermissionAttribute.PolicyPrefix.Length)
            .Split(",");

        return new AuthorizationPolicyBuilder()
            .AddRequirements(new PermissionRequirement(permissions))
            .Build();
    }
}
