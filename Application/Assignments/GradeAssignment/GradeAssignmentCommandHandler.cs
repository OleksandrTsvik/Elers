using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Queries;
using Application.Common.Services;
using Domain.Entities;
using Domain.Enums;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Assignments.GradeAssignment;

public class GradeAssignmentCommandHandler : ICommandHandler<GradeAssignmentCommand>
{
    private readonly ISubmittedAssignmentRepository _submittedAssignmentRepository;
    private readonly ICourseMaterialRepository _courseMaterialRepository;
    private readonly IGradeRepository _gradeRepository;
    private readonly ICourseMemberPermissionService _courseMemberPermissionService;
    private readonly ICourseQueries _courseQueries;
    private readonly IUserContext _userContext;

    public GradeAssignmentCommandHandler(
        ISubmittedAssignmentRepository submittedAssignmentRepository,
        ICourseMaterialRepository courseMaterialRepository,
        IGradeRepository gradeRepository,
        ICourseMemberPermissionService courseMemberPermissionService,
        ICourseQueries courseQueries,
        IUserContext userContext)
    {
        _submittedAssignmentRepository = submittedAssignmentRepository;
        _courseMaterialRepository = courseMaterialRepository;
        _gradeRepository = gradeRepository;
        _courseMemberPermissionService = courseMemberPermissionService;
        _courseQueries = courseQueries;
        _userContext = userContext;
    }

    public async Task<Result> Handle(GradeAssignmentCommand request, CancellationToken cancellationToken)
    {
        SubmittedAssignment? submittedAssignment = await _submittedAssignmentRepository
            .GetByIdAsync(request.SubmittedAssignmentId, cancellationToken);

        if (submittedAssignment is null)
        {
            return AssignmentErrors.SubmittedNotFound(request.SubmittedAssignmentId);
        }

        CourseMaterialAssignment? assignment = await _courseMaterialRepository
            .GetByIdAsync<CourseMaterialAssignment>(submittedAssignment.AssignmentId, cancellationToken);

        if (assignment is null)
        {
            return AssignmentErrors.NotFound(submittedAssignment.AssignmentId);
        }

        if (request.Grade > assignment.MaxGrade)
        {
            return AssignmentErrors.GradeLimit(assignment.MaxGrade);
        }

        if (!await _courseMemberPermissionService
            .CheckCoursePermissionsByCourseTabIdAsync(
                _userContext.UserId,
                assignment.CourseTabId,
                [CoursePermissionType.GradeCourseStudents],
                [PermissionType.GradeStudents]))
        {
            return AssignmentErrors.GradeAccessDenied();
        }

        Domain.Entities.GradeAssignment? grade = await _gradeRepository.GetByAssignmentIdAndStudentIdAsync(
            assignment.Id, submittedAssignment.StudentId, cancellationToken);

        if (grade is not null)
        {
            grade.Value = request.Grade;
            grade.CreatedAt = DateTime.UtcNow;
            grade.TeacherId = _userContext.UserId;

            await _gradeRepository.UpdateAsync(grade, cancellationToken);
        }
        else
        {
            Guid? courseId = await _courseQueries.GetCourseIdByCourseTabId(
                assignment.CourseTabId, cancellationToken);

            if (!courseId.HasValue)
            {
                return CourseErrors.NotFoundByTabId(assignment.CourseTabId);
            }

            var newGrade = new Domain.Entities.GradeAssignment
            {
                CourseId = courseId.Value,
                StudentId = submittedAssignment.StudentId,
                Value = request.Grade,
                CreatedAt = DateTime.UtcNow,
                TeacherId = _userContext.UserId,
                AssignmentId = assignment.Id
            };

            await _gradeRepository.AddAsync(newGrade, cancellationToken);
        }

        if (request.Status == SubmittedAssignmentStatus.Resubmit &&
            submittedAssignment.Status == SubmittedAssignmentStatus.Submitted)
        {
            submittedAssignment.AttemptNumber += 1;
        }

        submittedAssignment.TeacherId = _userContext.UserId;
        submittedAssignment.Status = request.Status;
        submittedAssignment.TeacherComment = request.Comment;

        await _submittedAssignmentRepository.UpdateAsync(submittedAssignment, cancellationToken);

        return Result.Success();
    }
}
