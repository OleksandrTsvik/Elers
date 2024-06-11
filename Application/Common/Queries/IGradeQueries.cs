using Application.Grades.DTOs;

namespace Application.Common.Queries;

public interface IGradeQueries
{
    Task<List<AssessmentItem>> GetAssessments(
        Guid courseId,
        bool onlyActive = true,
        CancellationToken cancellationToken = default);
}
