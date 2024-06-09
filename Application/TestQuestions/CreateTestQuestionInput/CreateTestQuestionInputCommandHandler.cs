using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.TestQuestions.CreateTestQuestionInput;

public class CreateTestQuestionInputCommandHandler : ICommandHandler<CreateTestQuestionInputCommand>
{
    private readonly ICourseMaterialRepository _courseMaterialRepository;
    private readonly ITestQuestionRepository _testQuestionRepository;

    public CreateTestQuestionInputCommandHandler(
        ICourseMaterialRepository courseMaterialRepository,
        ITestQuestionRepository testQuestionRepository)
    {
        _courseMaterialRepository = courseMaterialRepository;
        _testQuestionRepository = testQuestionRepository;
    }

    public async Task<Result> Handle(
        CreateTestQuestionInputCommand request,
        CancellationToken cancellationToken)
    {
        if (!await _courseMaterialRepository.ExistsByIdAsync(request.TestId, cancellationToken))
        {
            return TestErrors.NotFound(request.TestId);
        }

        var testQuestionInput = new TestQuestionInput
        {
            TestId = request.TestId,
            Text = request.Text,
            Points = request.Points,
            Answer = request.Answer
        };

        await _testQuestionRepository.AddAsync(testQuestionInput, cancellationToken);

        return Result.Success();
    }
}
