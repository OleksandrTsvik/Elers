using Application.Common.Messaging;

namespace Application.ManualGradesColumns.CreateManualGradesColumn;

public record CreateManualGradesColumnCommand(
    Guid CourseId,
    string Title,
    DateTime Date,
    int MaxGrade) : ICommand;
