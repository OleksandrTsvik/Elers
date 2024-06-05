using Application.Common.Messaging;
using Application.Users.DTOs;

namespace Application.Students.GetCourseStudents;

public record GetCourseStudentsQuery(Guid CourseId) : IQuery<UserDto[]>;
