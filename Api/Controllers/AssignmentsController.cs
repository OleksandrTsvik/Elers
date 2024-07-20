using Api.Contracts;
using Application.Assignments.DownloadAssignmentFile;
using Application.Assignments.GetAssignment;
using Application.Assignments.GetListAssignmentTitles;
using Application.Assignments.GetListSubmittedAssignments;
using Application.Assignments.GetSubmittedAssignment;
using Application.Assignments.GradeAssignment;
using Application.Assignments.SubmitAssignment;
using Domain.Enums;
using Infrastructure.CourseMemberPermissions;
using Infrastructure.Files;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class AssignmentsController : ApiControllerBase
{
    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAssignment(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetAssignmentQuery(id);

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [HttpGet("file/download/{fileName}")]
    public async Task<IActionResult> DownloadAssignmentFile(
        string fileName,
        CancellationToken cancellationToken)
    {
        var query = new DownloadAssignmentFileQuery(fileName);

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [HttpGet("titles/{courseId:guid}")]
    public async Task<IActionResult> GetListAssignmentTitles(
        Guid courseId,
        CancellationToken cancellationToken)
    {
        var query = new GetListAssignmentTitlesQuery(courseId);

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.GradeCourseStudents],
        [PermissionType.GradeStudents])]
    [HttpGet("submitted/{courseId:guid}")]
    public async Task<IActionResult> GetListSubmittedAssignments(
        Guid courseId,
        [FromQuery] GetListSubmittedAssignmentsQueryParams queryParams,
        CancellationToken cancellationToken)
    {
        var query = new GetListSubmittedAssignmentsQuery(courseId, queryParams);

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [HttpGet("review/{submittedAssignmentId:guid}")]
    public async Task<IActionResult> GetSubmittedAssignment(
        Guid submittedAssignmentId,
        CancellationToken cancellationToken)
    {
        var query = new GetSubmittedAssignmentQuery(submittedAssignmentId);

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [HttpPost("{id:guid}")]
    public async Task<IActionResult> SubmitAssignment(
        Guid id,
        [FromForm] SubmitAssignmentRequest request,
        CancellationToken cancellationToken)
    {
        var command = new SubmitAssignmentCommand(
            id,
            request.Text,
            request.Files?.Select(file => new FormFileProxy(file)).ToArray());

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HttpPost("grade/{submittedAssignmentId:guid}")]
    public async Task<IActionResult> GradeAssignment(
        Guid submittedAssignmentId,
        [FromBody] GradeAssignmentRequest request,
        CancellationToken cancellationToken)
    {
        var command = new GradeAssignmentCommand(
            submittedAssignmentId,
            request.Status,
            request.Grade,
            request.Comment);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }
}
