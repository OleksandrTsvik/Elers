using Application.Common.Messaging;

namespace Application.CourseMaterials.DeleteCourseMaterial;

public record DeleteCourseMaterialCommand(Guid Id) : ICommand;
