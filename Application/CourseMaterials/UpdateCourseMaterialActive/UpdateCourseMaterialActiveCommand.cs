using Application.Common.Messaging;

namespace Application.CourseMaterials.UpdateCourseMaterialActive;

public record UpdateCourseMaterialActiveCommand(Guid Id, bool IsActive) : ICommand;
