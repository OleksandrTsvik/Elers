using Application.Grades.DTOs;

namespace Application.Common.Queries;

public interface IGradeQueries
{
    Task<List<AssessmentItem>> GetAssessments(Guid courseId, CancellationToken cancellationToken = default);
}
