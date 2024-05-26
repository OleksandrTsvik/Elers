using Application.Common.Messaging;

namespace Application.Courses.DeleteCourse;

public record DeleteCourseCommand(Guid Id) : ICommand;
