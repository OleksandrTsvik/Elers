using Application.Common.Messaging;

namespace Application.CourseMembers.EnrollToCourse;

public record EnrollToCourseCommand(Guid CourseId) : ICommand;
