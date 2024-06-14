namespace Application.ManualGradesColumns.CreateManualGradesColumn;

public record CreateManualGradesColumnRequest(
    string Title,
    DateTime Date,
    int MaxGrade);
