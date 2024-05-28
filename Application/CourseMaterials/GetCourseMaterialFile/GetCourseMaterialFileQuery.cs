using Application.Common.Messaging;

namespace Application.CourseMaterials.GetCourseMaterialFile;

public record GetCourseMaterialFileQuery(Guid TabId, Guid Id)
    : IQuery<GetCourseMaterialFileResponse>;
