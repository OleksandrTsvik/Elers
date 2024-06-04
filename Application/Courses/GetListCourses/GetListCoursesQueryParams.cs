using Application.Common.Models;

namespace Application.Courses.GetListCourses;

public class GetListCoursesQueryParams : PagingParams
{
    public string? Search { get; init; }
}
