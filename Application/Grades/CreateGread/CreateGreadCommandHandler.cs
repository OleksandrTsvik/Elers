using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Services;
using Domain.Entities;
using Domain.Enums;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Grades.CreateGread;

public class CreateGreadCommandHandler : ICommandHandler<CreateGreadCommand>
{
    private readonly IGradeRepository _gradeRepository;
    private readonly IManualGradesColumnRepository _manualGradesColumnRepository;
    private readonly ICourseMemberPermissionService _courseMemberPermissionService;
    private readonly ICourseMemberService _courseMemberService;
    private readonly IUserContext _userContext;

    public CreateGreadCommandHandler(
        IGradeRepository gradeRepository,
        IManualGradesColumnRepository manualGradesColumnRepository,
        ICourseMemberPermissionService courseMemberPermissionService,
        ICourseMemberService courseMemberService,
        IUserContext userContext)
    {
        _gradeRepository = gradeRepository;
        _manualGradesColumnRepository = manualGradesColumnRepository;
        _courseMemberPermissionService = courseMemberPermissionService;
        _courseMemberService = courseMemberService;
        _userContext = userContext;
    }

    public async Task<Result> Handle(CreateGreadCommand request, CancellationToken cancellationToken)
    {
        Grade? grade = null;

        switch (request.GradeType)
        {
            case GradeType.Manual:
                ManualGradesColumn? column = await _manualGradesColumnRepository.GetByIdAsync(
                    request.AssessmentId, cancellationToken);

                if (column is null)
                {
                    return ManualGradesColumnErrors.NotFound(request.AssessmentId);
                }

                if (await _gradeRepository.ExistsByStudentIdAndColumnIdAsync(
                    request.StudentId, column.Id, cancellationToken))
                {
                    return GradeErrors.AlreadyExists();
                }

                if (!await CheckPermissionsAsync(column.CourseId))
                {
                    return GradeErrors.AccessDenied();
                }

                if (!await _courseMemberService.IsCourseStudentAsync(
                    request.StudentId, column.CourseId, cancellationToken))
                {
                    return GradeErrors.StudentsOnly();
                }

                if (request.Value > column.MaxGrade)
                {
                    return GradeErrors.GradeLimit(column.MaxGrade);
                }

                grade = new GradeManual
                {
                    CourseId = column.CourseId,
                    StudentId = request.StudentId,
                    ManualGradesColumnId = column.Id,
                    TeacherId = _userContext.UserId,
                    Value = request.Value
                };

                break;
        }

        if (grade is not null)
        {
            await _gradeRepository.AddAsync(grade, cancellationToken);
        }

        return Result.Success();
    }

    private Task<bool> CheckPermissionsAsync(Guid courseId) =>
        _courseMemberPermissionService.CheckCoursePermissionsAsync(
            _userContext.UserId,
            courseId,
            [CoursePermissionType.GradeCourseStudents],
            [PermissionType.GradeStudents]);
}
