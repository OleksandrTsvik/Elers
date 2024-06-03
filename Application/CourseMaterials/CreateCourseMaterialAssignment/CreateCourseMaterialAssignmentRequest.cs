namespace Application.CourseMaterials.CreateCourseMaterialAssignment;

public record CreateCourseMaterialAssignmentRequest(
    string Title,
    string Description,
    DateTime? Deadline,
    int MaxFiles,
    int MaxGrade);
