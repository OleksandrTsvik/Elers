using Domain.Primitives;

namespace Domain.Entities;

public class ManualGradesColumn : Entity
{
    public Guid CourseId { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public int MaxGrade { get; set; }
}
