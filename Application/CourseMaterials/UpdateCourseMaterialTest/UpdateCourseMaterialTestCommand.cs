using Application.Common.Messaging;

namespace Application.CourseMaterials.UpdateCourseMaterialTest;

public record UpdateCourseMaterialTestCommand(
    Guid MaterialId,
    string Title,
    string? Description,
    int NumberAttempts,
    int? TimeLimitInMinutes,
    DateTime? Deadline) : ICommand;
