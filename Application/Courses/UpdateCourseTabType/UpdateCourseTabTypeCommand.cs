using Application.Common.Messaging;

namespace Application.Courses.UpdateCourseTabType;

public record UpdateCourseTabTypeCommand(Guid CourseId, string? TabType) : ICommand;
