using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.ManualGradesColumns.CreateManualGradesColumn;

public class CreateManualGradesColumnCommandHandler : ICommandHandler<CreateManualGradesColumnCommand>
{
    private readonly IManualGradesColumnRepository _manualGradesColumnRepository;
    private readonly ICourseRepository _courseRepository;

    public CreateManualGradesColumnCommandHandler(
        IManualGradesColumnRepository manualGradesColumnRepository,
        ICourseRepository courseRepository)
    {
        _manualGradesColumnRepository = manualGradesColumnRepository;
        _courseRepository = courseRepository;
    }

    public async Task<Result> Handle(
        CreateManualGradesColumnCommand request,
        CancellationToken cancellationToken)
    {
        if (!await _courseRepository.ExistsByIdAsync(request.CourseId, cancellationToken))
        {
            return CourseErrors.NotFound(request.CourseId);
        }

        var column = new ManualGradesColumn
        {
            CourseId = request.CourseId,
            Title = request.Title,
            Date = request.Date,
            MaxGrade = request.MaxGrade
        };

        await _manualGradesColumnRepository.AddAsync(column, cancellationToken);

        return Result.Success();
    }
}
