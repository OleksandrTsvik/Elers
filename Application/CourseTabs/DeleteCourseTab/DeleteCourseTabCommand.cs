using Application.Common.Messaging;

namespace Application.CourseTabs.DeleteCourseTab;

public record DeleteCourseTabCommand(Guid CourseTabId) : ICommand;
