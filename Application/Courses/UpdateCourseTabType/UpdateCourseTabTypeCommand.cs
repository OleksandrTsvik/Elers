using Application.Common.Messaging;
using Domain.Enums;

namespace Application.Courses.UpdateCourseTabType;

public record UpdateCourseTabTypeCommand(Guid CourseId, CourseTabType TabType) : ICommand;
