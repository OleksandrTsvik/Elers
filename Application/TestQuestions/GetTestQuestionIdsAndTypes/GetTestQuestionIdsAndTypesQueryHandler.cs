using Application.Common.Messaging;
using Application.Common.Queries;
using Domain.Shared;

namespace Application.TestQuestions.GetTestQuestionIdsAndTypes;

public class GetTestQuestionIdsQueryHandler
    : IQueryHandler<GetTestQuestionIdsAndTypesQuery, List<GetTestQuestionIdsAndTypesResponse>>
{
    private readonly ITestQuestionQueries _testQuestionQueries;

    public GetTestQuestionIdsQueryHandler(ITestQuestionQueries testQuestionQueries)
    {
        _testQuestionQueries = testQuestionQueries;
    }

    public async Task<Result<List<GetTestQuestionIdsAndTypesResponse>>> Handle(
        GetTestQuestionIdsAndTypesQuery request,
        CancellationToken cancellationToken)
    {
        return await _testQuestionQueries.GetTestQuestionIdsByTestId(request.TestId, cancellationToken);
    }
}
