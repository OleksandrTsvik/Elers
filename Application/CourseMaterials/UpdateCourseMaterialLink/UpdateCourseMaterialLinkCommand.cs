using Application.Common.Messaging;

namespace Application.CourseMaterials.UpdateCourseMaterialLink;

public record UpdateCourseMaterialLinkCommand(
    Guid Id,
    string Title,
    string Link) : ICommand;
