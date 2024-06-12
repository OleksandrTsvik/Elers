using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.TestQuestions.UpdateTestQuestionMatching;

public class UpdateTestQuestionMatchingCommandHandler : ICommandHandler<UpdateTestQuestionMatchingCommand>
{
    private readonly ITestQuestionRepository _testQuestionRepository;

    public UpdateTestQuestionMatchingCommandHandler(ITestQuestionRepository testQuestionRepository)
    {
        _testQuestionRepository = testQuestionRepository;
    }

    public async Task<Result> Handle(
        UpdateTestQuestionMatchingCommand request,
        CancellationToken cancellationToken)
    {
        TestQuestionMatching? testQuestionMatching = await _testQuestionRepository
            .GetByIdAsync<TestQuestionMatching>(request.TestQuestionId, cancellationToken);

        if (testQuestionMatching is null)
        {
            return TestQuestionErrors.NotFound(request.TestQuestionId);
        }

        testQuestionMatching.Text = request.Text;
        testQuestionMatching.Points = request.Points;
        testQuestionMatching.Options = request.Options;

        await _testQuestionRepository.UpdateAsync(testQuestionMatching, cancellationToken);

        return Result.Success();
    }
}
