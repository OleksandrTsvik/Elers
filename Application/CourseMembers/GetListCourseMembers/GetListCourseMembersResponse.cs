namespace Application.CourseMembers.GetListCourseMembers;

public class CourseMemberListItem
{
    public required Guid Id { get; init; }
    public required Guid UserId { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Patronymic { get; init; }
    public required string? AvatarUrl { get; init; }
    public CourseMemberListItemRole? CourseRole { get; set; }
}

public class CourseMemberListItemRole
{
    public required Guid? Id { get; init; }
    public required string? Description { get; set; }
}
