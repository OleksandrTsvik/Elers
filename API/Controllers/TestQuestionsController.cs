using Application.TestQuestions.CreateTestQuestionInput;
using Application.TestQuestions.CreateTestQuestionMatching;
using Application.TestQuestions.CreateTestQuestionMultipleChoice;
using Application.TestQuestions.CreateTestQuestionSingleChoice;
using Application.TestQuestions.DeleteTestQuestion;
using Application.TestQuestions.GetTestQuestion;
using Application.TestQuestions.GetTestQuestionIdsAndTypes;
using Application.TestQuestions.UpdateTestQuestionInput;
using Application.TestQuestions.UpdateTestQuestionMatching;
using Application.TestQuestions.UpdateTestQuestionMultipleChoice;
using Application.TestQuestions.UpdateTestQuestionSingleChoice;
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

    [HasCourseMemberPermission(
        [CoursePermissionType.CreateTestQuestion],
        [PermissionType.ManageTestQuestions])]
    [HttpPost("single-choice/{materialId:guid}")]
    public async Task<IActionResult> CreateTestQuestionSingleChoice(
        Guid materialId,
        [FromBody] CreateTestQuestionSingleChoiceRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateTestQuestionSingleChoiceCommand(
            materialId,
            request.Text,
            request.Points,
            request.Options);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.UpdateTestQuestion],
        [PermissionType.ManageTestQuestions])]
    [HttpPut("single-choice/{testQuestionId:guid}")]
    public async Task<IActionResult> UpdateTestQuestionSingleChoice(
        Guid testQuestionId,
        [FromBody] UpdateTestQuestionSingleChoiceRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateTestQuestionSingleChoiceCommand(
            testQuestionId,
            request.Text,
            request.Points,
            request.Options);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.CreateTestQuestion],
        [PermissionType.ManageTestQuestions])]
    [HttpPost("multiple-choice/{materialId:guid}")]
    public async Task<IActionResult> CreateTestQuestionMultipleChoice(
        Guid materialId,
        [FromBody] CreateTestQuestionMultipleChoiceRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateTestQuestionMultipleChoiceCommand(
            materialId,
            request.Text,
            request.Points,
            request.Options);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.UpdateTestQuestion],
        [PermissionType.ManageTestQuestions])]
    [HttpPut("multiple-choice/{testQuestionId:guid}")]
    public async Task<IActionResult> UpdateTestQuestionMultipleChoice(
        Guid testQuestionId,
        [FromBody] UpdateTestQuestionMultipleChoiceRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateTestQuestionMultipleChoiceCommand(
            testQuestionId,
            request.Text,
            request.Points,
            request.Options);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.CreateTestQuestion],
        [PermissionType.ManageTestQuestions])]
    [HttpPost("matching/{materialId:guid}")]
    public async Task<IActionResult> CreateTestQuestionMatching(
        Guid materialId,
        [FromBody] CreateTestQuestionMatchingRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateTestQuestionMatchingCommand(
            materialId,
            request.Text,
            request.Points,
            request.Options);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.UpdateTestQuestion],
        [PermissionType.ManageTestQuestions])]
    [HttpPut("matching/{testQuestionId:guid}")]
    public async Task<IActionResult> UpdateTestQuestionMatching(
        Guid testQuestionId,
        [FromBody] UpdateTestQuestionMatchingRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateTestQuestionMatchingCommand(
            testQuestionId,
            request.Text,
            request.Points,
            request.Options);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.DeleteTestQuestion],
        [PermissionType.ManageTestQuestions])]
    [HttpDelete("{testQuestionId:guid}")]
    public async Task<IActionResult> DeleteTestQuestion(
        Guid testQuestionId,
        CancellationToken cancellationToken)
    {
        var command = new DeleteTestQuestionCommand(testQuestionId);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }
}
