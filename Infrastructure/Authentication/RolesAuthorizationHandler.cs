using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Authentication;

public class RolesAuthorizationHandler
    : AuthorizationHandler<RolesAuthorizationRequirement>, IAuthorizationHandler
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public RolesAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        RolesAuthorizationRequirement requirement)
    {
        string? userId = context.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userId, out Guid parsedUserId))
        {
            return;
        }

        using IServiceScope scope = _serviceScopeFactory.CreateScope();
        IRolesService rolesService = scope.ServiceProvider.GetRequiredService<IRolesService>();

        HashSet<string> userRoles = await rolesService.GetRolesAsync(parsedUserId);

        if (userRoles.Any(x => requirement.AllowedRoles.Contains(x)))
        {
            context.Succeed(requirement);
        }
    }
}
