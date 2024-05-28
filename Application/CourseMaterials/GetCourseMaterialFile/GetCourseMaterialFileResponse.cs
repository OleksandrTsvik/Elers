using Application.CourseMaterials.DTOs;

namespace Application.CourseMaterials.GetCourseMaterialFile;

public class GetCourseMaterialFileResponse : GetCourseMaterialResponseDto
{
    public required string FileTitle { get; set; }
}
