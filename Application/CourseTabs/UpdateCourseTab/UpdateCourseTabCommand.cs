using Application.Common.Messaging;

namespace Application.CourseTabs.UpdateCourseTab;

public record UpdateCourseTabCommand(
    Guid TabId,
    string? Name,
    bool? IsActive,
    bool? ShowMaterialsCount) : ICommand;
