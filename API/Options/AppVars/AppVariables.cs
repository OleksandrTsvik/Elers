using Domain.Constants;

namespace Api.Options.AppVars;

public class AppVariables : IAppVariables
{
    public required int MaxManualGrade { get; init; }
    public required int MaxAssignmentGrade { get; init; }
    public required int MaxFilesStudentUploadAssignment { get; init; }
}
