using Application.Common.Messaging;

namespace Application.CourseTabs.UpdateCourseTabName;

public record UpdateCourseTabNameCommand(Guid TabId, string Name) : ICommand;
