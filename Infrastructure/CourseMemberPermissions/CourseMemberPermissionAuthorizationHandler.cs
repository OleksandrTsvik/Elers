using System.Security.Claims;
using Application.Common.Services;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.CourseMemberPermissions;

public class CourseMemberPermissionAuthorizationHandler
    : AuthorizationHandler<CourseMemberPermissionRequirement>
{
    private const string RouteCourseId = "courseId";
    private const string RouteCourseTabId = "tabId";
    private const string RouteCourseMaterialId = "materialId";
    private const string RouteCourseRoleId = "roleId";
    private const string RouteCourseMemberId = "memberId";

    private readonly IServiceScopeFactory _serviceScopeFactory;

    public CourseMemberPermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        CourseMemberPermissionRequirement requirement)
    {
        string? userId = context.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userId, out Guid parsedUserId))
        {
            return;
        }

        string[] courseMemberPermissions = [];

        if (context.Resource is HttpContext httpContext)
        {
            (string? typeRouteId, string? routeId) = GetRouteValueId(httpContext.Request.RouteValues);

            (bool isCreator, string[] memberPermissions) = await GetMemberPermissions(
                parsedUserId, typeRouteId, routeId);

            if (isCreator)
            {
                context.Succeed(requirement);
                return;
            }

            courseMemberPermissions = memberPermissions;
        }

        if (courseMemberPermissions.Any(x => requirement.CourseMemberPermissions.Contains(x)))
        {
            context.Succeed(requirement);
            return;
        }

        if (requirement.UserPermissions.Length == 0)
        {
            return;
        }

        using IServiceScope scope = _serviceScopeFactory.CreateScope();
        IPermissionService permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();

        List<string> userPermissions = await permissionService.GetPermissionsAsync(parsedUserId);

        if (userPermissions.Any(x => requirement.UserPermissions.Contains(x)))
        {
            context.Succeed(requirement);
        }
    }

    private static (string? typeRouteId, string? routeId) GetRouteValueId(RouteValueDictionary routeValues)
    {
        string[] typeRouteIds = [
            RouteCourseId,
            RouteCourseTabId,
            RouteCourseMaterialId,
            RouteCourseRoleId,
            RouteCourseMemberId
        ];

        foreach (string typeRouteId in typeRouteIds)
        {
            if (routeValues.ContainsKey(typeRouteId))
            {
                return (typeRouteId, routeValues[typeRouteId]?.ToString());
            }
        }

        return (null, null);
    }

    private async Task<(bool isCreator, string[] memberPermissions)> GetMemberPermissions(
        Guid userId,
        string? typeRouteId,
        string? routeId)
    {
        if (!Guid.TryParse(routeId, out Guid parsedRouteId))
        {
            return (false, []);
        }

        using IServiceScope scope = _serviceScopeFactory.CreateScope();

        ICourseMemberPermissionService courseMemberPermissionService = scope.ServiceProvider
            .GetRequiredService<ICourseMemberPermissionService>();

        bool isCreator = typeRouteId switch
        {
            RouteCourseId => await courseMemberPermissionService
                .IsCreatorByCourseIdAsync(userId, parsedRouteId),
            RouteCourseTabId => await courseMemberPermissionService
                .IsCreatorByCourseTabIdAsync(userId, parsedRouteId),
            RouteCourseMaterialId => await courseMemberPermissionService
                .IsCreatorByCourseMaterialIdAsync(userId, parsedRouteId),
            RouteCourseRoleId => await courseMemberPermissionService
                .IsCreatorByCourseRoleIdAsync(userId, parsedRouteId),
            RouteCourseMemberId => await courseMemberPermissionService
                .IsCreatorByCourseMemberIdAsync(userId, parsedRouteId),
            _ => false
        };

        if (isCreator)
        {
            return (true, []);
        }

        string[] memberPermissions = typeRouteId switch
        {
            RouteCourseId => await courseMemberPermissionService
                .GetCourseMemberPermissionsByCourseIdAsync(userId, parsedRouteId),
            RouteCourseTabId => await courseMemberPermissionService
                .GetCourseMemberPermissionsByCourseTabIdAsync(userId, parsedRouteId),
            RouteCourseMaterialId => await courseMemberPermissionService
                .GetCourseMemberPermissionsByCourseMaterialIdAsync(userId, parsedRouteId),
            RouteCourseRoleId => await courseMemberPermissionService
                .GetCourseMemberPermissionsByCourseRoleIdAsync(userId, parsedRouteId),
            RouteCourseMemberId => await courseMemberPermissionService
                .GetCourseMemberPermissionsByCourseMemberIdAsync(userId, parsedRouteId),
            _ => []
        };

        return (isCreator, memberPermissions);
    }
}
