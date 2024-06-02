using Domain.Primitives;

namespace Domain.Entities;

public class CourseMember : Entity
{
    public Guid UserId { get; set; }
    public Guid CourseId { get; set; }
    public Guid? CourseRoleId { get; set; }
    public DateTime EnrollmentDate { get; set; }

    public User? User { get; set; }
    public Course? Course { get; set; }
    public CourseRole? CourseRole { get; set; }

    public CourseMember()
    {
        EnrollmentDate = DateTime.UtcNow;
    }
}
