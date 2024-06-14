using Application.Common.Messaging;
using Domain.Repositories;
using Domain.Shared;

namespace Application.ManualGradesColumns.DeleteManualGradesColumn;

public class DeleteManualGradesColumnCommandHandler : ICommandHandler<DeleteManualGradesColumnCommand>
{
    private readonly IManualGradesColumnRepository _manualGradesColumnRepository;
    private readonly IGradeRepository _gradeRepository;

    public DeleteManualGradesColumnCommandHandler(
        IManualGradesColumnRepository manualGradesColumnRepository,
        IGradeRepository gradeRepository)
    {
        _manualGradesColumnRepository = manualGradesColumnRepository;
        _gradeRepository = gradeRepository;
    }

    public async Task<Result> Handle(
        DeleteManualGradesColumnCommand request,
        CancellationToken cancellationToken)
    {
        await _manualGradesColumnRepository.RemoveAsync(request.Id, cancellationToken);
        await _gradeRepository.RemoveRangeByColumnIdAsync(request.Id, cancellationToken);

        return Result.Success();
    }
}
