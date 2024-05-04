using Application.Common.Messaging;

namespace Application.Courses.UpdateCourseTitle;

public record UpdateCourseTitleCommand(Guid CourseId, string Title) : ICommand;
