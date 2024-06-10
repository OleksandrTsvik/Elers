using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.TestQuestions.CreateTestQuestionMultipleChoice;

public class CreateTestQuestionMultipleChoiceCommandHandler
    : ICommandHandler<CreateTestQuestionMultipleChoiceCommand>
{
    private readonly ICourseMaterialRepository _courseMaterialRepository;
    private readonly ITestQuestionRepository _testQuestionRepository;

    public CreateTestQuestionMultipleChoiceCommandHandler(
        ICourseMaterialRepository courseMaterialRepository,
        ITestQuestionRepository testQuestionRepository)
    {
        _courseMaterialRepository = courseMaterialRepository;
        _testQuestionRepository = testQuestionRepository;
    }

    public async Task<Result> Handle(
        CreateTestQuestionMultipleChoiceCommand request,
        CancellationToken cancellationToken)
    {
        if (!await _courseMaterialRepository.ExistsByIdAsync(request.TestId, cancellationToken))
        {
            return TestErrors.NotFound(request.TestId);
        }

        var testQuestionMultipleChoice = new TestQuestionMultipleChoice
        {
            TestId = request.TestId,
            Text = request.Text,
            Points = request.Points,
            Options = request.Options
        };

        await _testQuestionRepository.AddAsync(testQuestionMultipleChoice, cancellationToken);

        return Result.Success();
    }
}
