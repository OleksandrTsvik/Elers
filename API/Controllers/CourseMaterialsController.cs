using API.Contracts;
using Application.CourseMaterials.CreateCourseMaterialAssignment;
using Application.CourseMaterials.CreateCourseMaterialContent;
using Application.CourseMaterials.CreateCourseMaterialFile;
using Application.CourseMaterials.CreateCourseMaterialLink;
using Application.CourseMaterials.CreateCourseMaterialTest;
using Application.CourseMaterials.DeleteCourseMaterial;
using Application.CourseMaterials.DownloadCourseMaterialFile;
using Application.CourseMaterials.GetCourseMaterialAssignment;
using Application.CourseMaterials.GetCourseMaterialContent;
using Application.CourseMaterials.GetCourseMaterialFile;
using Application.CourseMaterials.GetCourseMaterialLink;
using Application.CourseMaterials.GetCourseMaterialTest;
using Application.CourseMaterials.GetListCourseMaterialsByTabId;
using Application.CourseMaterials.GetListCourseMaterialsByTabIdToEdit;
using Application.CourseMaterials.UpdateCourseMaterialActive;
using Application.CourseMaterials.UpdateCourseMaterialAssignment;
using Application.CourseMaterials.UpdateCourseMaterialContent;
using Application.CourseMaterials.UpdateCourseMaterialFile;
using Application.CourseMaterials.UpdateCourseMaterialLink;
using Application.CourseMaterials.UpdateCourseMaterialTest;
using Domain.Enums;
using Infrastructure.CourseMemberPermissions;
using Infrastructure.Files;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CourseMaterialsController : ApiControllerBase
{
    [AllowAnonymous]
    [HttpGet("tabs/{tabId:guid}")]
    public async Task<IActionResult> GetListCourseMaterialsByTabId(
        Guid tabId,
        CancellationToken cancellationToken)
    {
        var query = new GetListCourseMaterialsByTabIdQuery(tabId);

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [AllowAnonymous]
    [HttpGet("file/download/{fileName}")]
    public async Task<IActionResult> DownloadCourseMaterialFile(
        string fileName,
        CancellationToken cancellationToken)
    {
        var query = new DownloadCourseMaterialFileQuery(fileName);

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [HasCourseMemberPermission(
        [
            CoursePermissionType.UpdateCourseMaterial,
            CoursePermissionType.CreateCourseMaterial,
            CoursePermissionType.DeleteCourseMaterial
        ],
        [PermissionType.ManageCourse])]
    [HttpGet("tabs/edit/{tabId:guid}")]
    public async Task<IActionResult> GetListCourseMaterialsByTabIdToEdit(
        Guid tabId,
        CancellationToken cancellationToken)
    {
        var query = new GetListCourseMaterialsByTabIdToEditQuery(tabId);

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.UpdateCourseMaterial],
        [PermissionType.ManageCourse])]
    [HttpGet("{tabId:guid}/content/{id:guid}")]
    public async Task<IActionResult> GetCourseMaterialContent(
        Guid tabId,
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetCourseMaterialContentQuery(tabId, id);

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.UpdateCourseMaterial],
        [PermissionType.ManageCourse])]
    [HttpGet("{tabId:guid}/link/{id:guid}")]
    public async Task<IActionResult> GetCourseMaterialLink(
        Guid tabId,
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetCourseMaterialLinkQuery(tabId, id);

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.UpdateCourseMaterial],
        [PermissionType.ManageCourse])]
    [HttpGet("{tabId:guid}/file/{id:guid}")]
    public async Task<IActionResult> GetCourseMaterialFile(
        Guid tabId,
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetCourseMaterialFileQuery(tabId, id);

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.UpdateCourseMaterial],
        [PermissionType.ManageCourse])]
    [HttpGet("assignment/{materialId:guid}")]
    public async Task<IActionResult> GetCourseMaterialAssignment(
        Guid materialId,
        CancellationToken cancellationToken)
    {
        var query = new GetCourseMaterialAssignmentQuery(materialId);

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.UpdateCourseMaterial],
        [PermissionType.ManageCourse])]
    [HttpGet("test/{materialId:guid}")]
    public async Task<IActionResult> GetCourseMaterialTest(
        Guid materialId,
        CancellationToken cancellationToken)
    {
        var query = new GetCourseMaterialTestQuery(materialId);

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.CreateCourseMaterial],
        [PermissionType.ManageCourse])]
    [HttpPost("content/{tabId:guid}")]
    public async Task<IActionResult> CreateCourseMaterialContent(
        Guid tabId,
        [FromBody] CreateCourseMaterialContentRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateCourseMaterialContentCommand(tabId, request.Content);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.UpdateCourseMaterial],
        [PermissionType.ManageCourse])]
    [HttpPut("content/{materialId:guid}")]
    public async Task<IActionResult> UpdateCourseMaterialContent(
        Guid materialId,
        [FromBody] UpdateCourseMaterialContentRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCourseMaterialContentCommand(materialId, request.Content);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.CreateCourseMaterial],
        [PermissionType.ManageCourse])]
    [HttpPost("link/{tabId:guid}")]
    public async Task<IActionResult> CreateCourseMaterialLink(
        Guid tabId,
        [FromBody] CreateCourseMaterialLinkRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateCourseMaterialLinkCommand(tabId, request.Title, request.Link);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.UpdateCourseMaterial],
        [PermissionType.ManageCourse])]
    [HttpPut("link/{materialId:guid}")]
    public async Task<IActionResult> UpdateCourseMaterialLink(
        Guid materialId,
        [FromBody] UpdateCourseMaterialLinkRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCourseMaterialLinkCommand(materialId, request.Title, request.Link);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.CreateCourseMaterial],
        [PermissionType.ManageCourse])]
    [HttpPost("file/{tabId:guid}")]
    public async Task<IActionResult> CreateCourseMaterialFile(
        Guid tabId,
        [FromForm] CreateCourseMaterialFileRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateCourseMaterialFileCommand(
            tabId,
            request.Title,
            new FormFileProxy(request.File));

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.UpdateCourseMaterial],
        [PermissionType.ManageCourse])]
    [HttpPut("file/{materialId:guid}")]
    public async Task<IActionResult> UpdateCourseMaterialFile(
        Guid materialId,
        [FromForm] UpdateCourseMaterialFileRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCourseMaterialFileCommand(
            materialId,
            request.Title,
            request.File is not null ? new FormFileProxy(request.File) : null);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.CreateCourseMaterial],
        [PermissionType.ManageCourse])]
    [HttpPost("assignment/{tabId:guid}")]
    public async Task<IActionResult> CreateCourseMaterialAssignment(
        Guid tabId,
        [FromBody] CreateCourseMaterialAssignmentRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateCourseMaterialAssignmentCommand(
            tabId,
            request.Title,
            request.Description,
            request.Deadline,
            request.MaxFiles,
            request.MaxGrade);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.UpdateCourseMaterial],
        [PermissionType.ManageCourse])]
    [HttpPut("assignment/{materialId:guid}")]
    public async Task<IActionResult> UpdateCourseMaterialAssignment(
        Guid materialId,
        [FromBody] UpdateCourseMaterialAssignmentRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCourseMaterialAssignmentCommand(
            materialId,
            request.Title,
            request.Description,
            request.Deadline,
            request.MaxFiles,
            request.MaxGrade);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.CreateCourseMaterial],
        [PermissionType.ManageCourse])]
    [HttpPost("test/{tabId:guid}")]
    public async Task<IActionResult> CreateCourseMaterialTest(
        Guid tabId,
        [FromBody] CreateCourseMaterialTestRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateCourseMaterialTestCommand(
            tabId,
            request.Title,
            request.Description,
            request.NumberAttempts,
            request.TimeLimitInMinutes,
            request.Deadline,
            request.GradingMethod,
            request.ShuffleQuestions);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.UpdateCourseMaterial],
        [PermissionType.ManageCourse])]
    [HttpPut("test/{materialId:guid}")]
    public async Task<IActionResult> UpdateCourseMaterialTest(
        Guid materialId,
        [FromBody] UpdateCourseMaterialTestRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCourseMaterialTestCommand(
            materialId,
            request.Title,
            request.Description,
            request.NumberAttempts,
            request.TimeLimitInMinutes,
            request.Deadline,
            request.GradingMethod,
            request.ShuffleQuestions);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.UpdateCourseMaterial],
        [PermissionType.ManageCourse])]
    [HttpPatch("active/{materialId:guid}")]
    public async Task<IActionResult> UpdateCourseMaterialActive(
        Guid materialId,
        [FromBody] UpdateCourseMaterialActiveRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCourseMaterialActiveCommand(materialId, request.IsActive);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.DeleteCourseMaterial],
        [PermissionType.ManageCourse])]
    [HttpDelete("{materialId:guid}")]
    public async Task<IActionResult> DeleteCourseMaterial(
        Guid materialId,
        CancellationToken cancellationToken)
    {
        var command = new DeleteCourseMaterialCommand(materialId);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }
}
