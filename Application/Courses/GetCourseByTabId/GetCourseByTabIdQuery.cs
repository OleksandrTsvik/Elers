using Application.Common.Messaging;

namespace Application.Courses.GetCourseByTabId;

public record GetCourseByTabIdQuery(Guid TabId) : IQuery<GetCourseByTabIdResponse>;
