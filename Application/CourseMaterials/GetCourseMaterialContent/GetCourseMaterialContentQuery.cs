using Application.Common.Messaging;

namespace Application.CourseMaterials.GetCourseMaterialContent;

public record GetCourseMaterialContentQuery(Guid TabId, Guid Id)
    : IQuery<GetCourseMaterialContentResponse>;
