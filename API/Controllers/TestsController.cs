using Application.Tests.GetTest;
using Application.Tests.GetTestSession;
using Application.Tests.GetTestSessionQuestion;
using Application.Tests.SendAnswerToTestQuestion;
using Application.Tests.StartTest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class TestsController : ApiControllerBase
{
    [AllowAnonymous]
    [HttpGet("{testId:guid}")]
    public async Task<IActionResult> GetTest(
        Guid testId,
        CancellationToken cancellationToken)
    {
        var query = new GetTestQuery(testId);

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [HttpGet("session/{testSessionId:guid}")]
    public async Task<IActionResult> GetTestSession(
        Guid testSessionId,
        CancellationToken cancellationToken)
    {
        var query = new GetTestSessionQuery(testSessionId);

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [HttpGet("{testSessionId:guid}/{questionId:guid}")]
    public async Task<IActionResult> GetTestSessionQuestion(
        Guid testSessionId,
        Guid questionId,
        CancellationToken cancellationToken)
    {
        var query = new GetTestSessionQuestionQuery(testSessionId, questionId);

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [HttpPost("{testId:guid}")]
    public async Task<IActionResult> StartTest(
        Guid testId,
        CancellationToken cancellationToken)
    {
        var command = new StartTestCommand(testId);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HttpPost("{testSessionId:guid}/{questionId:guid}")]
    public async Task<IActionResult> SendAnswerToTestQuestion(
        Guid testSessionId,
        Guid questionId,
        [FromBody] SendAnswerToTestQuestionRequest request,
        CancellationToken cancellationToken)
    {
        var command = new SendAnswerToTestQuestionCommand(
            testSessionId,
            questionId,
            request.Answer,
            request.Answers);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }
}
