using Application.Common.Messaging;
using Domain.Entities;

namespace Application.CourseMaterials.GetListCourseMaterials;

public class GetListCourseMaterialsQuery() : IQuery<List<CourseMaterial>>;
