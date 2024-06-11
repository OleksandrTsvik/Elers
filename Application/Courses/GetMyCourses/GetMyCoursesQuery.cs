using Application.Common.Messaging;
using Application.Common.Models;

namespace Application.Courses.GetMyCourses;

public record GetMyCoursesQuery(GetMyCoursesQueryParams QueryParams)
    : IQuery<PagedList<GetMyCourseItemResponse>>;
