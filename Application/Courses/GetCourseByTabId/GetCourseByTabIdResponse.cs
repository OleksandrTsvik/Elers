namespace Application.Courses.GetCourseByTabId;

public class GetCourseByTabIdResponse
{
    public Guid TabId { get; set; }
    public Guid? CourseId { get; set; }
    public string? Title { get; set; }
}
