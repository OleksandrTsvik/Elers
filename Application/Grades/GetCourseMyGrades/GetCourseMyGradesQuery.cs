using Application.Common.Messaging;

namespace Application.Grades.GetCourseMyGrades;

public record GetCourseMyGradesQuery(Guid CourseId) : IQuery<GetCourseMyGradeItemResponse[]>;
