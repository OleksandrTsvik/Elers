using Application.Common.Messaging;
using Domain.Enums;

namespace Application.CourseMaterials.UpdateCourseMaterialTest;

public record UpdateCourseMaterialTestCommand(
    Guid MaterialId,
    string Title,
    string? Description,
    int NumberAttempts,
    int? TimeLimitInMinutes,
    DateTime? Deadline,
    GradingMethod GradingMethod) : ICommand;
