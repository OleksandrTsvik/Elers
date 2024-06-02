using Application.Common.Models;

namespace Application.CourseMembers.GetListCourseMembers;

public class GetListCourseMembersQueryParams : PagingParams
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Patronymic { get; init; }
    public string? SortColumn { get; set; }
    public SortOrder? SortOrder { get; set; }
    public Guid[]? Roles { get; set; }
}
