using System.Text.Json.Serialization;
using Application.Grades.DTOs;
using Application.Users.DTOs;
using Domain.Enums;

namespace Application.Grades.GetCourseMyGrades;

public class GetCourseMyGradeItemResponse
{
    public required AssessmentItem Assessment { get; init; }
    public required CourseMyGrade? Grade { get; init; }
}

[JsonDerivedType(typeof(CourseMyGradeAssignment))]
public class CourseMyGrade
{
    public required Guid AssessmentId { get; init; }
    public required double? Grade { get; init; }
    public required GradeType Type { get; init; }
    public required DateTime CreatedAt { get; init; }
}

public class CourseMyGradeAssignment : CourseMyGrade
{
    public required UserDto? Teacher { get; init; }
}

public class CourseTestMyGrade
{
    public Guid TestId { get; set; }
    public int? TimeLimitInMinutes { get; set; }
}
