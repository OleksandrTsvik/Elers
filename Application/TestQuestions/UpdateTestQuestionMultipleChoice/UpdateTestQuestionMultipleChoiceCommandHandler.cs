using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.TestQuestions.UpdateTestQuestionMultipleChoice;

public class UpdateTestQuestionMultipleChoiceCommandHandler
    : ICommandHandler<UpdateTestQuestionMultipleChoiceCommand>
{
    private readonly ITestQuestionRepository _testQuestionRepository;

    public UpdateTestQuestionMultipleChoiceCommandHandler(ITestQuestionRepository testQuestionRepository)
    {
        _testQuestionRepository = testQuestionRepository;
    }

    public async Task<Result> Handle(
        UpdateTestQuestionMultipleChoiceCommand request,
        CancellationToken cancellationToken)
    {
        TestQuestionMultipleChoice? testQuestionMultipleChoice = await _testQuestionRepository
            .GetByIdAsync<TestQuestionMultipleChoice>(request.TestQuestionId, cancellationToken);

        if (testQuestionMultipleChoice is null)
        {
            return TestQuestionErrors.NotFound(request.TestQuestionId);
        }

        testQuestionMultipleChoice.Text = request.Text;
        testQuestionMultipleChoice.Points = request.Points;
        testQuestionMultipleChoice.Options = request.Options;

        await _testQuestionRepository.UpdateAsync(testQuestionMultipleChoice, cancellationToken);

        return Result.Success();
    }
}
