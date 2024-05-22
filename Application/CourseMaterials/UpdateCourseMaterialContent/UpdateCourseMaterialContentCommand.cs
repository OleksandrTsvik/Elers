using Application.Common.Messaging;

namespace Application.CourseMaterials.UpdateCourseMaterialContent;

public record UpdateCourseMaterialContentCommand(Guid Id, string Content) : ICommand;
