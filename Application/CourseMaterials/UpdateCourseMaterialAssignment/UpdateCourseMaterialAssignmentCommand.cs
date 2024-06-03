using Application.Common.Messaging;

namespace Application.CourseMaterials.UpdateCourseMaterialAssignment;

public record UpdateCourseMaterialAssignmentCommand(
    Guid MaterialId,
    string Title,
    string Description,
    DateTime? Deadline,
    int MaxFiles,
    int MaxGrade) : ICommand;
