using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.TestQuestions.UpdateTestQuestionSingleChoice;

public class UpdateTestQuestionSingleChoiceCommandHandler
    : ICommandHandler<UpdateTestQuestionSingleChoiceCommand>
{
    private readonly ITestQuestionRepository _testQuestionRepository;

    public UpdateTestQuestionSingleChoiceCommandHandler(ITestQuestionRepository testQuestionRepository)
    {
        _testQuestionRepository = testQuestionRepository;
    }

    public async Task<Result> Handle(
        UpdateTestQuestionSingleChoiceCommand request,
        CancellationToken cancellationToken)
    {
        TestQuestionSingleChoice? testQuestionSingleChoice = await _testQuestionRepository
            .GetByIdAsync<TestQuestionSingleChoice>(request.TestQuestionId, cancellationToken);

        if (testQuestionSingleChoice is null)
        {
            return TestQuestionErrors.NotFound(request.TestQuestionId);
        }

        testQuestionSingleChoice.Text = request.Text;
        testQuestionSingleChoice.Points = request.Points;
        testQuestionSingleChoice.Options = request.Options;

        await _testQuestionRepository.UpdateAsync(testQuestionSingleChoice, cancellationToken);

        return Result.Success();
    }
}
