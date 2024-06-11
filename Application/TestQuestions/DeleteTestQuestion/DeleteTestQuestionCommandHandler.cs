using Application.Common.Messaging;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.TestQuestions.DeleteTestQuestion;

public class DeleteTestQuestionCommandHandler : ICommandHandler<DeleteTestQuestionCommand>
{
    private readonly ITestQuestionRepository _testQuestionRepository;
    private readonly ITestSessionRespository _testSessionRespository;

    public DeleteTestQuestionCommandHandler(
        ITestQuestionRepository testQuestionRepository,
        ITestSessionRespository testSessionRespository)
    {
        _testQuestionRepository = testQuestionRepository;
        _testSessionRespository = testSessionRespository;
    }

    public async Task<Result> Handle(DeleteTestQuestionCommand request, CancellationToken cancellationToken)
    {
        if (!await _testQuestionRepository.ExistsByIdAsync(request.TestQuestionId, cancellationToken))
        {
            return TestQuestionErrors.NotFound(request.TestQuestionId);
        }

        await _testSessionRespository.RemoveQuestionAsync(request.TestQuestionId, cancellationToken);

        await _testQuestionRepository.RemoveAsync(request.TestQuestionId, cancellationToken);

        return Result.Success();
    }
}
