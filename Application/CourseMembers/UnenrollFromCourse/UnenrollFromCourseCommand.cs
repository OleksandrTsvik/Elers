using Application.Common.Messaging;

namespace Application.CourseMembers.UnenrollFromCourse;

public record UnenrollFromCourseCommand(Guid CourseId) : ICommand;
