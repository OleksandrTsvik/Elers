using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Grades.UpdateGread;

public class UpdateGreadCommandHandler : ICommandHandler<UpdateGreadCommand>
{
    private readonly IGradeRepository _gradeRepository;
    private readonly IManualGradesColumnRepository _manualGradesColumnRepository;
    private readonly ICourseMaterialRepository _courseMaterialRepository;
    private readonly IUserContext _userContext;

    public UpdateGreadCommandHandler(
        IGradeRepository gradeRepository,
        IManualGradesColumnRepository manualGradesColumnRepository,
        ICourseMaterialRepository courseMaterialRepository,
        IUserContext userContext)
    {
        _gradeRepository = gradeRepository;
        _manualGradesColumnRepository = manualGradesColumnRepository;
        _courseMaterialRepository = courseMaterialRepository;
        _userContext = userContext;
    }

    public async Task<Result> Handle(UpdateGreadCommand request, CancellationToken cancellationToken)
    {
        Grade? grade = await _gradeRepository.GetByIdAsync(request.GradeId, cancellationToken);

        if (grade is null)
        {
            return GradeErrors.NotFound(request.GradeId);
        }

        switch (grade)
        {
            case GradeManual gradeManual:
                ManualGradesColumn? column = await _manualGradesColumnRepository
                    .GetByIdAsync(gradeManual.ManualGradesColumnId, cancellationToken);

                if (column is null)
                {
                    return ManualGradesColumnErrors.NotFound(gradeManual.ManualGradesColumnId);
                }

                if (request.Value > column.MaxGrade)
                {
                    return GradeErrors.GradeLimit(column.MaxGrade);
                }

                gradeManual.TeacherId = _userContext.UserId;
                gradeManual.Value = request.Value;

                break;
            case GradeAssignment gradeAssignment:
                CourseMaterialAssignment? assignment = await _courseMaterialRepository
                    .GetByIdAsync<CourseMaterialAssignment>(gradeAssignment.AssignmentId, cancellationToken);

                if (assignment is null)
                {
                    return AssignmentErrors.NotFound(gradeAssignment.AssignmentId);
                }

                if (request.Value > assignment.MaxGrade)
                {
                    return GradeErrors.GradeLimit(assignment.MaxGrade);
                }

                gradeAssignment.TeacherId = _userContext.UserId;
                gradeAssignment.Value = request.Value;

                break;
        }

        grade.CreatedAt = DateTime.UtcNow;

        await _gradeRepository.UpdateAsync(grade, cancellationToken);

        return Result.Success();
    }
}
