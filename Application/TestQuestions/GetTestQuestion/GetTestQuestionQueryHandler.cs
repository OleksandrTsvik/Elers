using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.TestQuestions.GetTestQuestion;

public class GetTestQuestionQueryHandler : IQueryHandler<GetTestQuestionQuery, TestQuestion>
{
    private readonly ITestQuestionRepository _testQuestionRepository;

    public GetTestQuestionQueryHandler(ITestQuestionRepository testQuestionRepository)
    {
        _testQuestionRepository = testQuestionRepository;
    }

    public async Task<Result<TestQuestion>> Handle(
        GetTestQuestionQuery request,
        CancellationToken cancellationToken)
    {
        TestQuestion? testQuestion = await _testQuestionRepository.GetByIdAsync(
            request.TestQuestionId, cancellationToken);

        if (testQuestion is null)
        {
            return TestQuestionErrors.NotFound(request.TestQuestionId);
        }

        return testQuestion;
    }
}
