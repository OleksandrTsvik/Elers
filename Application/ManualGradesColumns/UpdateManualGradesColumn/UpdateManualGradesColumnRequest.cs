namespace Application.ManualGradesColumns.UpdateManualGradesColumn;

public record UpdateManualGradesColumnRequest(
    string Title,
    DateTime Date,
    int MaxGrade);
