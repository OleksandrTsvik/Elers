using Domain.Enums;

namespace Application.Common.Services;

public interface ICourseMemberPermissionService
{
    Task<string[]> GetCourseMemberPermissionsByCourseIdAsync(Guid userId, Guid courseId);

    Task<string[]> GetCourseMemberPermissionsByCourseTabIdAsync(Guid userId, Guid courseTabId);

    Task<string[]> GetCourseMemberPermissionsByCourseMaterialIdAsync(Guid userId, Guid courseMaterialId);

    Task<string[]> GetCourseMemberPermissionsByCourseRoleIdAsync(Guid userId, Guid courseRoleId);

    Task<bool> IsCreatorByCourseIdAsync(Guid userId, Guid courseId);

    Task<bool> IsCreatorByCourseTabIdAsync(Guid userId, Guid courseTabId);

    Task<bool> IsCreatorByCourseMaterialIdAsync(Guid userId, Guid courseMaterialId);

    Task<bool> IsCreatorByCourseRoleIdAsync(Guid userId, Guid courseRoleId);

    Task<bool> CheckCoursePermissionsAsync(
        Guid userId,
        Guid courseId,
        IEnumerable<CoursePermissionType> courseMemberPermissions,
        IEnumerable<PermissionType> userPermissions);
}
