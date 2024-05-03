using Application.Common.Messaging;

namespace Application.Courses.GetCourseById;

public record GetCourseByIdQuery(Guid Id) : IQuery<GetCourseByIdResponse>;
