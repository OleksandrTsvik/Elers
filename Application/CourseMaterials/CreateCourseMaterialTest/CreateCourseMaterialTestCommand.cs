using Application.Common.Messaging;
using Domain.Enums;

namespace Application.CourseMaterials.CreateCourseMaterialTest;

public record CreateCourseMaterialTestCommand(
    Guid CourseTabId,
    string Title,
    string? Description,
    int NumberAttempts,
    int? TimeLimitInMinutes,
    DateTime? Deadline,
    GradingMethod GradingMethod) : ICommand<Guid>;
