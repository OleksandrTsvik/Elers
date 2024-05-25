using Application.Common.Messaging;
using Domain.Entities;

namespace Application.CourseMaterials.GetListCourseMaterialsByTabIdToEdit;

public record GetListCourseMaterialsByTabIdToEditQuery(Guid TabId) : IQuery<List<CourseMaterial>>;
