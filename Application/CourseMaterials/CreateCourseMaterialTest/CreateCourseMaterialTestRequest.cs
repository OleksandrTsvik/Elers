using Domain.Enums;

namespace Application.CourseMaterials.CreateCourseMaterialTest;

public record CreateCourseMaterialTestRequest(
    string Title,
    string? Description,
    int NumberAttempts,
    int? TimeLimitInMinutes,
    DateTime? Deadline,
    GradingMethod GradingMethod);
