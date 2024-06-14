using Domain.Enums;

namespace Application.Common.Services;

public interface ICourseMemberPermissionService
{
    Task<string[]> GetCourseMemberPermissionsByCourseIdAsync(Guid userId, Guid courseId);

    Task<string[]> GetCourseMemberPermissionsByCourseTabIdAsync(Guid userId, Guid courseTabId);

    Task<string[]> GetCourseMemberPermissionsByCourseMaterialIdAsync(Guid userId, Guid courseMaterialId);

    Task<string[]> GetCourseMemberPermissionsByCourseRoleIdAsync(Guid userId, Guid courseRoleId);

    Task<string[]> GetCourseMemberPermissionsByCourseMemberIdAsync(Guid userId, Guid courseMemberId);

    Task<string[]> GetCourseMemberPermissionsByTestQuestionIdAsync(Guid userId, Guid testQuestionId);

    Task<string[]> GetCourseMemberPermissionsByGradeIdAsync(Guid userId, Guid gradeId);

    Task<string[]> GetCourseMemberPermissionsByColumnGradesIdAsync(Guid userId, Guid columnGradesId);

    Task<bool> IsCreatorByCourseIdAsync(Guid userId, Guid courseId);

    Task<bool> IsCreatorByCourseTabIdAsync(Guid userId, Guid courseTabId);

    Task<bool> IsCreatorByCourseMaterialIdAsync(Guid userId, Guid courseMaterialId);

    Task<bool> IsCreatorByCourseRoleIdAsync(Guid userId, Guid courseRoleId);

    Task<bool> IsCreatorByCourseMemberIdAsync(Guid userId, Guid courseMemberId);

    Task<bool> IsCreatorByTestQuestionIdAsync(Guid userId, Guid testQuestionId);

    Task<bool> IsCreatorByGradeIdAsync(Guid userId, Guid gradeId);

    Task<bool> IsCreatorByColumnGradesIdAsync(Guid userId, Guid columnGradesId);

    Task<bool> CheckCoursePermissionsAsync(
        Guid userId,
        Guid courseId,
        IEnumerable<CoursePermissionType> courseMemberPermissions,
        IEnumerable<PermissionType> userPermissions);

    Task<bool> CheckCoursePermissionsByCourseTabIdAsync(
        Guid userId,
        Guid courseTabId,
        IEnumerable<CoursePermissionType> courseMemberPermissions,
        IEnumerable<PermissionType> userPermissions);
}
