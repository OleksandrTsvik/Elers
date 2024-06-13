using Application.Common.Messaging;
using Domain.Shared;

namespace Application.CourseMaterials.ReorderCourseMaterials;

public record ReorderCourseMaterialsCommand(ReorderItem[] Reorders) : ICommand;
