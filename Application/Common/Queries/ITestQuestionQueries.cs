using Application.TestQuestions.DTOs;

namespace Application.Common.Queries;

public interface ITestQuestionQueries
{
    Task<List<TestQuestionIdsAndTypesDto>> GetTestQuestionIdsAndTypesByTestId(
        Guid testId,
        CancellationToken cancellationToken = default);
}
