using Application.Common.Messaging;

namespace Application.CourseTabs.CreateCourseTab;

public record CreateCourseTabCommand(Guid CourseId, string Name) : ICommand<CreateCourseTabResponse>;
