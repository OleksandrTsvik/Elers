namespace Application.CourseTabs.UpdateCourseTab;

public record UpdateCourseTabRequest(
    string? Name,
    bool? IsActive,
    bool? ShowMaterialsCount);
