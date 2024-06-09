using Application.TestQuestions.GetTestQuestionIdsAndTypes;

namespace Application.Common.Queries;

public interface ITestQuestionQueries
{
    Task<List<GetTestQuestionIdsAndTypesResponse>> GetTestQuestionIdsByTestId(
        Guid testId,
        CancellationToken cancellationToken = default);
}
