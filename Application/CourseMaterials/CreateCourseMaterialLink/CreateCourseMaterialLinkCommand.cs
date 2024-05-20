using Application.Common.Messaging;

namespace Application.CourseMaterials.CreateCourseMaterialLink;

public record CreateCourseMaterialLinkCommand(
    Guid TabId,
    string Title,
    string Link) : ICommand;
