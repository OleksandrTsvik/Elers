using Application.Common.Messaging;

namespace Application.CourseMaterials.CreateCourseMaterialContent;

public record CreateCourseMaterialContentCommand(Guid TabId, string Content) : ICommand;
