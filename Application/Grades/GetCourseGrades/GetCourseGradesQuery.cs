using Application.Common.Messaging;

namespace Application.Grades.GetCourseGrades;

public record GetCourseGradesQuery(Guid CourseId) : IQuery<GetCourseGradesResponse>;
