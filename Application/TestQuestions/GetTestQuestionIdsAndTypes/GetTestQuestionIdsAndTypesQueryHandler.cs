using Application.Common.Messaging;
using Application.Common.Queries;
using Application.TestQuestions.DTOs;
using Domain.Shared;

namespace Application.TestQuestions.GetTestQuestionIdsAndTypes;

public class GetTestQuestionIdsQueryHandler
    : IQueryHandler<GetTestQuestionIdsAndTypesQuery, List<TestQuestionIdsAndTypesDto>>
{
    private readonly ITestQuestionQueries _testQuestionQueries;

    public GetTestQuestionIdsQueryHandler(ITestQuestionQueries testQuestionQueries)
    {
        _testQuestionQueries = testQuestionQueries;
    }

    public async Task<Result<List<TestQuestionIdsAndTypesDto>>> Handle(
        GetTestQuestionIdsAndTypesQuery request,
        CancellationToken cancellationToken)
    {
        return await _testQuestionQueries.GetTestQuestionIdsAndTypesByTestId(
            request.TestId, cancellationToken);
    }
}
