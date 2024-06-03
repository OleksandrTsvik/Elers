namespace Application.CourseMaterials.UpdateCourseMaterialAssignment;

public record UpdateCourseMaterialAssignmentRequest(
    string Title,
    string Description,
    DateTime? Deadline,
    int MaxFiles,
    int MaxGrade);
