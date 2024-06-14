using Application.Common.Messaging;

namespace Application.Grades.UpdateGread;

public record UpdateGreadCommand(Guid GradeId, double Value) : ICommand;
