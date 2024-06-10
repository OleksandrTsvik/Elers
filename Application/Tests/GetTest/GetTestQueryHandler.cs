using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Queries;
using Application.Tests.DTOs;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Tests.GetTest;

public class GetTestQueryHandler : IQueryHandler<GetTestQuery, GetTestResponse>
{
    private readonly ICourseMaterialRepository _courseMaterialRepository;
    private readonly ITestQueries _testQueries;
    private readonly IUserContext _userContext;

    public GetTestQueryHandler(
        ICourseMaterialRepository courseMaterialRepository,
        ITestQueries testQueries,
        IUserContext userContext)
    {
        _courseMaterialRepository = courseMaterialRepository;
        _testQueries = testQueries;
        _userContext = userContext;
    }

    public async Task<Result<GetTestResponse>> Handle(
        GetTestQuery request,
        CancellationToken cancellationToken)
    {
        CourseMaterialTest? test = await _courseMaterialRepository
            .GetByIdAsync<CourseMaterialTest>(request.TestId, cancellationToken);

        if (test is null)
        {
            return TestErrors.NotFound(request.TestId);
        }

        if (!test.IsActive)
        {
            return TestErrors.NotActive();
        }

        var attempts = new List<TestAttemptItem>();

        if (_userContext.IsAuthenticated)
        {
            List<TestSessionDto> testSessions = await _testQueries.GetTestSessionDtosByTestIdAndUserId(
                test.Id, _userContext.UserId, cancellationToken);

            attempts = testSessions
                .Select(x => new TestAttemptItem
                {
                    TestSessionId = x.Id,
                    StartedAt = x.StartedAt,
                    FinishedAt = x.FinishedAt,
                    Grade = null,
                    IsCompleted = IsCompletedSession(x, test.TimeLimitInMinutes),
                })
                .ToList();
        }

        return new GetTestResponse
        {
            TestId = test.Id,
            CourseTabId = test.CourseTabId,
            Title = test.Title,
            Description = test.Description,
            NumberAttempts = test.NumberAttempts,
            TimeLimitInMinutes = test.TimeLimitInMinutes,
            Deadline = test.Deadline,
            Attempts = attempts
        };
    }

    private static bool IsCompletedSession(TestSessionDto testSession, int? timeLimitInMinutes) =>
        testSession.FinishedAt is not null ||
        (timeLimitInMinutes.HasValue &&
        testSession.StartedAt.AddMinutes(timeLimitInMinutes.Value) < DateTime.UtcNow);
}
