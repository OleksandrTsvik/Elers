using Domain.Enums;
using Domain.Primitives;

namespace Domain.Entities;

public class SubmittedAssignment : Entity
{
    public Guid AssignmentId { get; set; }
    public Guid StudentId { get; set; }
    public Guid? TeacherId { get; set; }
    public SubmittedAssignmentStatus Status { get; set; }
    public int AttemptNumber { get; set; }
    public string? Text { get; set; }
    public List<SubmitAssignmentFile> Files { get; set; } = [];
    public string? TeacherComment { get; set; }
    public DateTime SubmittedAt { get; set; }

    public SubmittedAssignment()
    {
        AttemptNumber = 1;
    }
}

public class SubmitAssignmentFile
{
    public string FileName { get; set; } = string.Empty;
    public string UniqueFileName { get; set; } = string.Empty;
}
