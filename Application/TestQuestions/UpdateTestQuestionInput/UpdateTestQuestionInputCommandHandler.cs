using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.TestQuestions.UpdateTestQuestionInput;

public class UpdateTestQuestionInputCommandHandler : ICommandHandler<UpdateTestQuestionInputCommand>
{
    private readonly ITestQuestionRepository _testQuestionRepository;

    public UpdateTestQuestionInputCommandHandler(ITestQuestionRepository testQuestionRepository)
    {
        _testQuestionRepository = testQuestionRepository;
    }

    public async Task<Result> Handle(
        UpdateTestQuestionInputCommand request,
        CancellationToken cancellationToken)
    {
        TestQuestionInput? testQuestionInput = await _testQuestionRepository
            .GetByIdAsync<TestQuestionInput>(request.TestQuestionId, cancellationToken);

        if (testQuestionInput is null)
        {
            return TestQuestionErrors.NotFound(request.TestQuestionId);
        }

        testQuestionInput.Text = request.Text;
        testQuestionInput.Points = request.Points;
        testQuestionInput.Answer = request.Answer;

        await _testQuestionRepository.UpdateAsync(testQuestionInput, cancellationToken);

        return Result.Success();
    }
}
