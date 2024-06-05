using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Queries;
using Application.Users.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Assignments.GetAssignment;

public class GetAssignmentQueryHandler : IQueryHandler<GetAssignmentQuery, GetAssignmentResponse>
{
    private readonly ICourseMaterialRepository _courseMaterialRepository;
    private readonly ISubmittedAssignmentRepository _submittedAssignmentRepository;
    private readonly IGradeRepository _gradeRepository;
    private readonly IUserQueries _userQueries;
    private readonly IUserContext _userContext;

    public GetAssignmentQueryHandler(
        ICourseMaterialRepository courseMaterialRepository,
        ISubmittedAssignmentRepository submittedAssignmentRepository,
        IGradeRepository gradeRepository,
        IUserQueries userQueries,
        IUserContext userContext)
    {
        _courseMaterialRepository = courseMaterialRepository;
        _submittedAssignmentRepository = submittedAssignmentRepository;
        _gradeRepository = gradeRepository;
        _userQueries = userQueries;
        _userContext = userContext;
    }

    public async Task<Result<GetAssignmentResponse>> Handle(
        GetAssignmentQuery request,
        CancellationToken cancellationToken)
    {
        CourseMaterialAssignment? assignment = await _courseMaterialRepository
            .GetByIdAsync<CourseMaterialAssignment>(request.AssignmentId, cancellationToken);

        if (assignment is null)
        {
            return AssignmentErrors.NotFound(request.AssignmentId);
        }

        if (!assignment.IsActive)
        {
            return AssignmentErrors.Unavailable(request.AssignmentId);
        }

        var response = new GetAssignmentResponse
        {
            AssignmentId = assignment.Id,
            CourseTabId = assignment.CourseTabId,
            Title = assignment.Title,
            Description = assignment.Description,
            Deadline = assignment.Deadline,
            MaxFiles = assignment.MaxFiles,
            MaxGrade = assignment.MaxGrade
        };

        if (_userContext.IsAuthenticated)
        {
            SubmittedAssignment? submittedAssignment = await _submittedAssignmentRepository
                .GetByAssignmentIdAndStudentIdAsync(
                    request.AssignmentId,
                    _userContext.UserId,
                    cancellationToken);

            if (submittedAssignment is not null)
            {
                UserDto? teacher = null;
                double? grade = null;

                if (submittedAssignment.Status == SubmittedAssignmentStatus.Graded)
                {
                    grade = await _gradeRepository.GetValueByAssignmentIdAndStudentIdAsync(
                        assignment.Id, _userContext.UserId, cancellationToken);
                }

                if (submittedAssignment.TeacherId.HasValue)
                {
                    teacher = await _userQueries.GetUserDtoById(
                        submittedAssignment.TeacherId.Value, cancellationToken);
                }

                response.SubmittedAssignment = new SubmittedAssignmentDto
                {
                    Teacher = teacher,
                    Status = submittedAssignment.Status,
                    AttemptNumber = submittedAssignment.AttemptNumber,
                    Grade = grade,
                    TeacherComment = submittedAssignment.TeacherComment,
                    Text = submittedAssignment.Text,
                    Files = submittedAssignment.Files,
                    SubmittedAt = submittedAssignment.SubmittedAt
                };
            }
        }

        return response;
    }
}
