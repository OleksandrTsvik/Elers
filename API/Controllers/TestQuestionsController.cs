using Application.TestQuestions.CreateTestQuestionInput;
using Application.TestQuestions.GetTestQuestion;
using Application.TestQuestions.GetTestQuestionIdsAndTypes;
using Application.TestQuestions.UpdateTestQuestionInput;
using Domain.Enums;
using Infrastructure.CourseMemberPermissions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class TestQuestionsController : ApiControllerBase
{
    [HasCourseMemberPermission(
        [CoursePermissionType.UpdateTestQuestion, CoursePermissionType.DeleteTestQuestion],
        [PermissionType.ManageTestQuestions])]
    [HttpGet("{testQuestionId:guid}")]
    public async Task<IActionResult> GetTestQuestion(
        Guid testQuestionId,
        CancellationToken cancellationToken)
    {
        var query = new GetTestQuestionQuery(testQuestionId);

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.UpdateTestQuestion, CoursePermissionType.DeleteTestQuestion],
        [PermissionType.ManageTestQuestions])]
    [HttpGet("ids-and-types/{materialId:guid}")]
    public async Task<IActionResult> GetTestQuestionIdsAndTypes(
        Guid materialId,
        CancellationToken cancellationToken)
    {
        var query = new GetTestQuestionIdsAndTypesQuery(materialId);

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.CreateTestQuestion],
        [PermissionType.ManageTestQuestions])]
    [HttpPost("input/{materialId:guid}")]
    public async Task<IActionResult> CreateTestQuestionInput(
        Guid materialId,
        [FromBody] CreateTestQuestionInputRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateTestQuestionInputCommand(
            materialId,
            request.Text,
            request.Points,
            request.Answer);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.UpdateTestQuestion],
        [PermissionType.ManageTestQuestions])]
    [HttpPut("input/{testQuestionId:guid}")]
    public async Task<IActionResult> UpdateTestQuestionInput(
        Guid testQuestionId,
        [FromBody] UpdateTestQuestionInputRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateTestQuestionInputCommand(
            testQuestionId,
            request.Text,
            request.Points,
            request.Answer);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }
}
