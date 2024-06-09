using Application.Common.Messaging;

namespace Application.CourseMaterials.CreateCourseMaterialTest;

public record CreateCourseMaterialTestCommand(
    Guid CourseTabId,
    string Title,
    string? Description,
    int NumberAttempts,
    int? TimeLimitInMinutes,
    DateTime? Deadline) : ICommand<Guid>;
