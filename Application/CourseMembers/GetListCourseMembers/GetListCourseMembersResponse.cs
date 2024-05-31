namespace Application.CourseMembers.GetListCourseMembers;

public class GetListCourseMemberItemResponse
{
    public required Guid Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Patronymic { get; init; }
    public required string? AvatarUrl { get; init; }
    public GetListCourseMemberItemRoleResponse? CourseRole { get; set; }
}

public class GetListCourseMemberItemRoleResponse
{
    public required Guid? Id { get; init; }
    public required string? Description { get; set; }
}
