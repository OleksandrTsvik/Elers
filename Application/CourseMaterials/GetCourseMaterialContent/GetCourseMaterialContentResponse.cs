using Application.CourseMaterials.DTOs;

namespace Application.CourseMaterials.GetCourseMaterialContent;

public class GetCourseMaterialContentResponse : GetCourseMaterialResponseDto
{
    public required string Content { get; init; }
}
