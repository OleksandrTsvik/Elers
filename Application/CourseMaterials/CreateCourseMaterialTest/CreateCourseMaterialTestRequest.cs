namespace Application.CourseMaterials.CreateCourseMaterialTest;

public record CreateCourseMaterialTestRequest(
    string Title,
    string? Description,
    int NumberAttempts,
    int? TimeLimitInMinutes,
    DateTime? Deadline);
