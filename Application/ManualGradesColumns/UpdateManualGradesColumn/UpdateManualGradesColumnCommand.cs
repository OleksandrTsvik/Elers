using Application.Common.Messaging;

namespace Application.ManualGradesColumns.UpdateManualGradesColumn;

public record UpdateManualGradesColumnCommand(
    Guid Id,
    string Title,
    DateTime Date,
    int MaxGrade) : ICommand;
