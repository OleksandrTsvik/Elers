using Domain.Shared;

namespace Application.CourseMaterials.ReorderCourseMaterials;

public record ReorderCourseMaterialsRequest(ReorderItem[] Reorders);
