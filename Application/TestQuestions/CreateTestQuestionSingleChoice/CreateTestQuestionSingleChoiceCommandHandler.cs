using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.TestQuestions.CreateTestQuestionSingleChoice;

public class CreateTestQuestionSingleChoiceCommandHandler
    : ICommandHandler<CreateTestQuestionSingleChoiceCommand>
{
    private readonly ICourseMaterialRepository _courseMaterialRepository;
    private readonly ITestQuestionRepository _testQuestionRepository;

    public CreateTestQuestionSingleChoiceCommandHandler(
        ICourseMaterialRepository courseMaterialRepository,
        ITestQuestionRepository testQuestionRepository)
    {
        _courseMaterialRepository = courseMaterialRepository;
        _testQuestionRepository = testQuestionRepository;
    }

    public async Task<Result> Handle(
        CreateTestQuestionSingleChoiceCommand request,
        CancellationToken cancellationToken)
    {
        if (!await _courseMaterialRepository.ExistsByIdAsync(request.TestId, cancellationToken))
        {
            return TestErrors.NotFound(request.TestId);
        }

        var testQuestionSingleChoice = new TestQuestionSingleChoice
        {
            TestId = request.TestId,
            Text = request.Text,
            Points = request.Points,
            Options = request.Options
        };

        await _testQuestionRepository.AddAsync(testQuestionSingleChoice, cancellationToken);

        return Result.Success();
    }
}
