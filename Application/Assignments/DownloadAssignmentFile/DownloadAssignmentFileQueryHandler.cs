using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Models;
using Application.Common.Services;
using Domain.Entities;
using Domain.Enums;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Assignments.DownloadAssignmentFile;

public class DownloadAssignmentFileQueryHandler
    : IQueryHandler<DownloadAssignmentFileQuery, FileDownloadResult>
{
    private readonly ISubmittedAssignmentRepository _submittedAssignmentRepository;
    private readonly ICourseMaterialRepository _courseMaterialRepository;
    private readonly ICourseMemberPermissionService _courseMemberPermissionService;
    private readonly IFileService _fileService;
    private readonly IUserContext _userContext;

    public DownloadAssignmentFileQueryHandler(
        ISubmittedAssignmentRepository submittedAssignmentRepository,
        ICourseMaterialRepository courseMaterialRepository,
        ICourseMemberPermissionService courseMemberPermissionService,
        IFileService fileService,
        IUserContext userContext)
    {
        _submittedAssignmentRepository = submittedAssignmentRepository;
        _courseMaterialRepository = courseMaterialRepository;
        _courseMemberPermissionService = courseMemberPermissionService;
        _fileService = fileService;
        _userContext = userContext;
    }

    public async Task<Result<FileDownloadResult>> Handle(
        DownloadAssignmentFileQuery request,
        CancellationToken cancellationToken)
    {
        SubmittedAssignment? submittedAssignment = await _submittedAssignmentRepository
            .GetByUniqueFileNameAsync(request.FileName, cancellationToken);

        if (submittedAssignment is null)
        {
            return AssignmentErrors.FileNotFound();
        }

        CourseMaterialAssignment? assignment = await _courseMaterialRepository
            .GetByIdAsync<CourseMaterialAssignment>(submittedAssignment.AssignmentId, cancellationToken);

        if (assignment is null)
        {
            return AssignmentErrors.NotFound(submittedAssignment.AssignmentId);
        }

        if (_userContext.UserId != submittedAssignment.StudentId && !await _courseMemberPermissionService
            .CheckCoursePermissionsByCourseTabIdAsync(
                _userContext.UserId,
                assignment.CourseTabId,
                [CoursePermissionType.GradeCourseStudents],
                [PermissionType.GradeStudents]))
        {
            return AssignmentErrors.FileAccessDenied();
        }

        SubmitAssignmentFile? file = submittedAssignment.Files
            .Where(x => x.UniqueFileName == request.FileName)
            .FirstOrDefault();

        if (file is null)
        {
            return AssignmentErrors.FileNotFound();
        }

        byte[] fileContents = await _fileService.DownloadAsync(file.UniqueFileName, cancellationToken);

        return new FileDownloadResult
        {
            FileName = file.FileName,
            FileContents = fileContents
        };
    }
}
