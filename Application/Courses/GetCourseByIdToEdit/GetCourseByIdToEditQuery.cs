using Application.Common.Messaging;

namespace Application.Courses.GetCourseByIdToEdit;

public record GetCourseByIdToEditQuery(Guid Id)
    : IQuery<GetCourseByIdToEditResponse<CourseTabToEditResponse>>;
