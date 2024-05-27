using Application.Common.Messaging;

namespace Application.Courses.DeleteCourseImage;

public record DeleteCourseImageCommand(Guid Id) : ICommand;
