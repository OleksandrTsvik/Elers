using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Models;
using Application.Common.Services;
using Domain.Entities;
using Domain.Enums;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Assignments.SubmitAssignment;

public class SubmitAssignmentCommandHandler : ICommandHandler<SubmitAssignmentCommand>
{
    private readonly ICourseMaterialRepository _courseMaterialRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly ISubmittedAssignmentRepository _submittedAssignmentRepository;
    private readonly ICourseService _courseService;
    private readonly IFileService _fileService;
    private readonly IUserContext _userContext;

    public SubmitAssignmentCommandHandler(
        IStudentRepository studentRepository,
        ISubmittedAssignmentRepository submittedAssignmentRepository,
        ICourseMaterialRepository courseMaterialRepository,
        ICourseService courseService,
        IFileService fileService,
        IUserContext userContext)
    {
        _studentRepository = studentRepository;
        _courseMaterialRepository = courseMaterialRepository;
        _submittedAssignmentRepository = submittedAssignmentRepository;
        _courseService = courseService;
        _fileService = fileService;
        _userContext = userContext;
    }

    public async Task<Result> Handle(SubmitAssignmentCommand request, CancellationToken cancellationToken)
    {
        Student? student = await _studentRepository.GetByIdAsync(_userContext.UserId, cancellationToken);

        if (student is null)
        {
            return AssignmentErrors.StudentsOnly();
        }

        CourseMaterialAssignment? assignment = await _courseMaterialRepository
            .GetByIdAsync<CourseMaterialAssignment>(request.AssignmentId, cancellationToken);

        if (assignment is null)
        {
            return AssignmentErrors.NotFound(request.AssignmentId);
        }

        if (assignment.Deadline.HasValue && assignment.Deadline.Value.AddDays(1).Date < DateTime.UtcNow.Date)
        {
            return AssignmentErrors.DeadlinePassed();
        }

        if (!await _courseService.IsCourseMemberByCourseTabIdAsync(
            student.Id, assignment.CourseTabId, cancellationToken))
        {
            return AssignmentErrors.StudentsOnly();
        }

        SubmittedAssignment? submittedAssignment = await _submittedAssignmentRepository
            .GetByAssignmentIdAndStudentIdAsync(assignment.Id, student.Id, cancellationToken);

        if (submittedAssignment?.Status == SubmittedAssignmentStatus.Graded)
        {
            return AssignmentErrors.AlreadyGraded();
        }

        if (string.IsNullOrEmpty(request.Text) && (request.Files is null || request.Files.Length == 0))
        {
            return AssignmentErrors.EmptyFields();
        }

        var files = new List<SubmitAssignmentFile>();

        if (request.Files is not null)
        {
            if (request.Files.Length > assignment.MaxFiles)
            {
                return AssignmentErrors.ManyFiles(assignment.MaxFiles);
            }

            foreach (IFile file in request.Files)
            {
                Result<FileUploadResult> addFileResult = await _fileService.AddAsync(file, cancellationToken);

                if (addFileResult.IsFailure || addFileResult.Value is null)
                {
                    return addFileResult.Error;
                }

                files.Add(new SubmitAssignmentFile
                {
                    FileName = addFileResult.Value.FileName,
                    UniqueFileName = addFileResult.Value.UniqueFileName
                });
            }
        }

        if (submittedAssignment is null)
        {
            var newSubmittedAssignment = new SubmittedAssignment
            {
                AssignmentId = assignment.Id,
                StudentId = student.Id,
                Status = SubmittedAssignmentStatus.Submitted,
                Text = request.Text,
                Files = files,
                SubmittedAt = DateTime.UtcNow
            };

            await _submittedAssignmentRepository.AddAsync(newSubmittedAssignment, cancellationToken);
        }
        else
        {
            if (submittedAssignment.Files.Count != 0)
            {
                await _fileService.RemoveRangeAsync(
                    submittedAssignment.Files.Select(x => x.UniqueFileName), cancellationToken);
            }

            submittedAssignment.Status = SubmittedAssignmentStatus.Submitted;
            submittedAssignment.Text = request.Text;
            submittedAssignment.Files = files;
            submittedAssignment.SubmittedAt = DateTime.UtcNow;

            await _submittedAssignmentRepository.UpdateAsync(submittedAssignment, cancellationToken);
        }

        return Result.Success();
    }
}
