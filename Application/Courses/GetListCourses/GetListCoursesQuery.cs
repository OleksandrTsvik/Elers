using Application.Common.Messaging;
using Application.Common.Models;

namespace Application.Courses.GetListCourses;

public record GetListCoursesQuery(GetListCoursesQueryParams QueryParams)
    : IQuery<PagedList<GetListCourseItemResponse>>;
