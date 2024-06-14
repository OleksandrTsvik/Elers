using Application.Common.Messaging;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.ManualGradesColumns.UpdateManualGradesColumn;

public class UpdateManualGradesColumnCommandHandler : ICommandHandler<UpdateManualGradesColumnCommand>
{
    private readonly IManualGradesColumnRepository _manualGradesColumnRepository;

    public UpdateManualGradesColumnCommandHandler(IManualGradesColumnRepository manualGradesColumnRepository)
    {
        _manualGradesColumnRepository = manualGradesColumnRepository;
    }

    public async Task<Result> Handle(
        UpdateManualGradesColumnCommand request,
        CancellationToken cancellationToken)
    {
        Domain.Entities.ManualGradesColumn? column = await _manualGradesColumnRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (column is null)
        {
            return ManualGradesColumnErrors.NotFound(request.Id);
        }

        column.Title = request.Title;
        column.Date = request.Date;
        column.MaxGrade = request.MaxGrade;

        await _manualGradesColumnRepository.UpdateAsync(column, cancellationToken);

        return Result.Success();
    }
}
