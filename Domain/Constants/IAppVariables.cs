namespace Domain.Constants;

public interface IAppVariables
{
    int MaxManualGrade { get; init; }
    int MaxAssignmentGrade { get; init; }
    int MaxFilesStudentUploadAssignment { get; init; }
}
