using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Queries;
using Application.Tests.DTOs;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Tests.FinishTest;

public class FinishTestCommandHandler : ICommandHandler<FinishTestCommand>
{
    private readonly ITestSessionRespository _testSessionRespository;
    private readonly ITestQueries _testQueries;
    private readonly IUserContext _userContext;

    public FinishTestCommandHandler(
        ITestSessionRespository testSessionRespository,
        ITestQueries testQueries,
        IUserContext userContext)
    {
        _testSessionRespository = testSessionRespository;
        _testQueries = testQueries;
        _userContext = userContext;
    }

    public async Task<Result> Handle(FinishTestCommand request, CancellationToken cancellationToken)
    {
        TestSessionDto? testSession = await _testQueries.GetTestSessionDtoByIdAndUserId(
            request.TestSessionId, _userContext.UserId, cancellationToken);

        if (testSession is null)
        {
            return TestErrors.UserSessionNotFound();
        }

        if (testSession.FinishedAt.HasValue)
        {
            return TestErrors.AttemptAlreadyCompleted();
        }

        await _testSessionRespository.UpdateFinishedAtAsync(
            request.TestSessionId, DateTime.UtcNow, cancellationToken);

        return Result.Success();
    }
}
