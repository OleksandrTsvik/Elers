using Application.Common.Messaging;

namespace Application.CourseMaterials.CreateCourseMaterialAssignment;

public record CreateCourseMaterialAssignmentCommand(
    Guid TabId,
    string Title,
    string Description,
    DateTime? Deadline,
    int MaxFiles,
    int MaxGrade) : ICommand;
