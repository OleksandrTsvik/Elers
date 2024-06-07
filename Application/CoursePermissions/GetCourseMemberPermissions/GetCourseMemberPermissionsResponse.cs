namespace Application.CoursePermissions.GetCourseMemberPermissions;

public class GetCourseMemberPermissionsResponse
{
    public required bool IsCreator { get; init; }
    public required bool IsMember { get; init; }
    public required bool IsStudent { get; init; }
    public required string[] MemberPermissions { get; init; }
}
