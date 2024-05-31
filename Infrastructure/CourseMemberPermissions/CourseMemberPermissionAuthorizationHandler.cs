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

        string[]? courseMemberPermissions = null;

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

        if (courseMemberPermissions is not null &&
            courseMemberPermissions.Any(x => requirement.CourseMemberPermissions.Contains(x)))
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
        string[] typeRouteIds = [RouteCourseId, RouteCourseTabId, RouteCourseMaterialId];

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

        bool isCreator = false;

        switch (typeRouteId)
        {
            case RouteCourseId:
                isCreator = await courseMemberPermissionService.IsCreatorByCourseIdAsync(
                    userId, parsedRouteId);
                break;
            case RouteCourseTabId:
                isCreator = await courseMemberPermissionService.IsCreatorByCourseTabIdAsync(
                    userId, parsedRouteId);
                break;
            case RouteCourseMaterialId:
                isCreator = await courseMemberPermissionService.IsCreatorByCourseMaterialIdAsync(
                    userId, parsedRouteId);
                break;
            case RouteCourseRoleId:
                isCreator = await courseMemberPermissionService.IsCreatorByCourseRoleIdAsync(
                    userId, parsedRouteId);
                break;
        };

        if (isCreator)
        {
            return (true, []);
        }

        string[] memberPermissions = [];

        switch (typeRouteId)
        {
            case RouteCourseId:
                memberPermissions = await courseMemberPermissionService
                    .GetCourseMemberPermissionsByCourseIdAsync(userId, parsedRouteId);
                break;
            case RouteCourseTabId:
                memberPermissions = await courseMemberPermissionService
                    .GetCourseMemberPermissionsByCourseTabIdAsync(userId, parsedRouteId);
                break;
            case RouteCourseMaterialId:
                memberPermissions = await courseMemberPermissionService
                    .GetCourseMemberPermissionsByCourseMaterialIdAsync(userId, parsedRouteId);
                break;
            case RouteCourseRoleId:
                memberPermissions = await courseMemberPermissionService
                    .GetCourseMemberPermissionsByCourseRoleIdAsync(userId, parsedRouteId);
                break;
        };

        return (isCreator, memberPermissions);
    }
}
