using Application.Common.Messaging;
using Domain.Entities;

namespace Application.CourseMaterials.GetListCourseMaterialsByTabId;

public record GetListCourseMaterialsByTabIdQuery(Guid TabId) : IQuery<List<CourseMaterial>>;
