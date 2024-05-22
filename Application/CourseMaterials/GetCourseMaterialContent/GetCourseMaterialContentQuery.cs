using Application.Common.Messaging;
using Application.CourseMaterials.DTOs;

namespace Application.CourseMaterials.GetCourseMaterialContent;

public record GetCourseMaterialContentQuery(Guid TabId, Guid Id)
    : IQuery<GetCourseMaterialContentResponseDto>;
