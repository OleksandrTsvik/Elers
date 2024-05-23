using Application.CourseMaterials.DTOs;

namespace Application.CourseMaterials.GetCourseMaterialLink;

public class GetCourseMaterialLinkResponse : GetCourseMaterialResponseDto
{
    public required string Title { get; set; }
    public required string Link { get; set; }
}
