using System.Text.Json.Serialization;
using Application.Grades.DTOs;
using Application.Users.DTOs;
using Domain.Enums;

namespace Application.Grades.GetCourseGrades;

public class GetCourseGradesResponse
{
    public required IEnumerable<AssessmentItem> Assessments { get; init; }
    public required List<CourseGradeItemResponse> StudentGrades { get; init; }
}

public class CourseGradeItemResponse
{
    public required UserDto Student { get; init; }
    public required List<GradeItemResponse> Grades { get; init; }
}

[JsonDerivedType(typeof(GradeAssignmentItemResponse))]
public class GradeItemResponse
{
    public required Guid AssessmentId { get; init; }
    public required Guid GradeId { get; init; }
    public required double? Grade { get; init; }
    public required GradeType Type { get; init; }
    public required DateTime CreatedAt { get; init; }
}

public class GradeAssignmentItemResponse : GradeItemResponse
{
    public required UserDto? Teacher { get; init; }
}
