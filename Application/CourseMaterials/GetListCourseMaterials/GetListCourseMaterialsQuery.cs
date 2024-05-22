using Application.Common.Messaging;
using Domain.Entities;

namespace Application.CourseMaterials.GetListCourseMaterials;

public record GetListCourseMaterialsQuery() : IQuery<List<CourseMaterial>>;
