using Application.Common.Messaging;

namespace Application.Courses.GetListCourses;

public record GetListCoursesQuery() : IQuery<GetListCourseItemResponse[]>;
