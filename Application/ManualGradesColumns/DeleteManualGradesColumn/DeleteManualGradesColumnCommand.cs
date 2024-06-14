using Application.Common.Messaging;

namespace Application.ManualGradesColumns.DeleteManualGradesColumn;

public record DeleteManualGradesColumnCommand(Guid Id) : ICommand;
