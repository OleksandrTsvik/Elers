using Application.Common.Messaging;

namespace Application.CourseTabs.UpdateCourseTabColor;

public record UpdateCourseTabColorCommand(Guid TabId, string? Color) : ICommand;
