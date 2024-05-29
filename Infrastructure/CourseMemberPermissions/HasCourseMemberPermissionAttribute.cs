using Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.CourseMemberPermissions;

/// <summary>
/// Attribute for checking the course member's permissions to access the controller's methods.
/// </summary>
/// <remarks>
/// This attribute should be used on controller methods that have one of the following parameters
/// in the reference: <c>courseId</c>, <c>tabId</c>, or <c>materialId</c>.
/// </remarks>
public class HasCourseMemberPermissionAttribute : AuthorizeAttribute
{
    public const string CourseMemberPolicyPrefix = "COURSE_MEMBER_PERMISSION:";
    public const string UserPolicyPrefix = "USER_PERMISSION:";
    public const string Separator = "---";

    public HasCourseMemberPermissionAttribute(
         CoursePermissionType[] courseMemberPermissions,
         PermissionType[]? userPermissions = null)
    {
        string courseMemberPolicy = $"{CourseMemberPolicyPrefix}{string.Join(",", courseMemberPermissions)}";
        string userPolicy = $"{UserPolicyPrefix}{string.Join(",", userPermissions ?? [])}";

        Policy = $"{courseMemberPolicy}{Separator}{userPolicy}";
    }
}
