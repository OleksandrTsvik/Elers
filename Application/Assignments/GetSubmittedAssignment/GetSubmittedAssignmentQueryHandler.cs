using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Queries;
using Application.Common.Services;
using Application.Users.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Assignments.GetSubmittedAssignment;

public class GetSubmittedAssignmentQueryHandler
    : IQueryHandler<GetSubmittedAssignmentQuery, GetSubmittedAssignmentResponse>
{
    private readonly ISubmittedAssignmentRepository _submittedAssignmentRepository;
    private readonly ICourseMaterialRepository _courseMaterialRepository;
    private readonly IGradeRepository _gradeRepository;
    private readonly IUserQueries _userQueries;
    private readonly ICourseMemberPermissionService _courseMemberPermissionService;
    private readonly IUserContext _userContext;

    public GetSubmittedAssignmentQueryHandler(
        ISubmittedAssignmentRepository submittedAssignmentRepository,
        ICourseMaterialRepository courseMaterialRepository,
        ICourseMemberPermissionService courseMemberPermissionService,
        IGradeRepository gradeRepository,
        IUserQueries userQueries,
        IUserContext userContext)
    {
        _submittedAssignmentRepository = submittedAssignmentRepository;
        _courseMaterialRepository = courseMaterialRepository;
        _courseMemberPermissionService = courseMemberPermissionService;
        _gradeRepository = gradeRepository;
        _userQueries = userQueries;
        _userContext = userContext;
    }

    public async Task<Result<GetSubmittedAssignmentResponse>> Handle(
        GetSubmittedAssignmentQuery request,
        CancellationToken cancellationToken)
    {
        SubmittedAssignment? submittedAssignment = await _submittedAssignmentRepository
            .GetByIdAsync(request.SubmittedAssignmentId, cancellationToken);

        if (submittedAssignment is null)
        {
            return AssignmentErrors.SubmittedNotFound(request.SubmittedAssignmentId);
        }

        UserDto? student = await _userQueries.GetUserDtoById(submittedAssignment.StudentId, cancellationToken);

        if (student is null)
        {
            return StudentErrors.NotFound(submittedAssignment.StudentId);
        }

        CourseMaterialAssignment? assignment = await _courseMaterialRepository
            .GetByIdAsync<CourseMaterialAssignment>(submittedAssignment.AssignmentId, cancellationToken);

        if (assignment is null)
        {
            return AssignmentErrors.NotFound(submittedAssignment.AssignmentId);
        }

        if (!await _courseMemberPermissionService
            .CheckCoursePermissionsByCourseTabIdAsync(
                _userContext.UserId,
                assignment.CourseTabId,
                [CoursePermissionType.GradeCourseStudents],
                [PermissionType.GradeStudents]))
        {
            return AssignmentErrors.SubmittedAccessDenied();
        }

        double? grade = await _gradeRepository.GetValueByAssignmentIdAndStudentIdAsync(
            assignment.Id, submittedAssignment.StudentId, cancellationToken);

        UserDto? teacher = null;

        if (submittedAssignment.TeacherId.HasValue)
        {
            teacher = await _userQueries.GetUserDtoById(
                submittedAssignment.TeacherId.Value, cancellationToken);
        }

        return new GetSubmittedAssignmentResponse
        {
            SubmittedAssignmentId = submittedAssignment.Id,
            AssignmentId = assignment.Id,
            Title = assignment.Title,
            Description = assignment.Description,
            Deadline = assignment.Deadline,
            MaxFiles = assignment.MaxFiles,
            MaxGrade = assignment.MaxGrade,
            Student = student,
            Teacher = teacher,
            Grade = grade,
            Status = submittedAssignment.Status,
            AttemptNumber = submittedAssignment.AttemptNumber,
            TeacherComment = submittedAssignment.TeacherComment,
            Text = submittedAssignment.Text,
            Files = submittedAssignment.Files,
            SubmittedAt = submittedAssignment.SubmittedAt,
        };
    }
}
