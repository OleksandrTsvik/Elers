using Application.Common.Messaging;
using Domain.Shared;

namespace Application.CourseTabs.ReorderCourseTabs;

public record ReorderCourseTabsCommand(ReorderItem[] Reorders) : ICommand;
