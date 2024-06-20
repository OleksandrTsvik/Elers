using Application.Common.Models;
using Domain.Enums;

namespace Application.CourseMembers.GetListCourseMembers;

public class GetListCourseMembersQueryParams : PagingParams
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Patronymic { get; init; }
    public string? SortColumn { get; init; }
    public SortOrder? SortOrder { get; init; }
    public Guid[]? Roles { get; init; }
    public UserType[]? UserTypes { get; init; }
}
