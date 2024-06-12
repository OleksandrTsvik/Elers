using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.TestQuestions.CreateTestQuestionMatching;

public class CreateTestQuestionMatchingCommandHandler : ICommandHandler<CreateTestQuestionMatchingCommand>
{
    private readonly ICourseMaterialRepository _courseMaterialRepository;
    private readonly ITestQuestionRepository _testQuestionRepository;

    public CreateTestQuestionMatchingCommandHandler(
        ICourseMaterialRepository courseMaterialRepository,
        ITestQuestionRepository testQuestionRepository)
    {
        _courseMaterialRepository = courseMaterialRepository;
        _testQuestionRepository = testQuestionRepository;
    }

    public async Task<Result> Handle(
        CreateTestQuestionMatchingCommand request,
        CancellationToken cancellationToken)
    {
        if (!await _courseMaterialRepository.ExistsByIdAsync(request.TestId, cancellationToken))
        {
            return TestErrors.NotFound(request.TestId);
        }

        var testQuestionMatching = new TestQuestionMatching
        {
            TestId = request.TestId,
            Text = request.Text,
            Points = request.Points,
            Options = request.Options
        };

        await _testQuestionRepository.AddAsync(testQuestionMatching, cancellationToken);

        return Result.Success();
    }
}
