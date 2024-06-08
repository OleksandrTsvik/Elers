namespace Application.CourseMaterials.UpdateCourseMaterialTest;

public record UpdateCourseMaterialTestRequest(
    string Title,
    string? Description,
    int NumberAttempts,
    int? TimeLimitInMinutes,
    DateTime? Deadline);
