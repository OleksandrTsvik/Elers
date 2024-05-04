using Application.Common.Messaging;

namespace Application.Courses.UpdateCourseDescription;

public record UpdateCourseDescriptionCommand(Guid CourseId, string? Description) : ICommand;
