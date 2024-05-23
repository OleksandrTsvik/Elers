using Application.Common.Messaging;

namespace Application.CourseMaterials.GetCourseMaterialLink;

public record GetCourseMaterialLinkQuery(Guid TabId, Guid Id)
    : IQuery<GetCourseMaterialLinkResponse>;
