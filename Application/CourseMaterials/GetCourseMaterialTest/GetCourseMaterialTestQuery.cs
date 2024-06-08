using Application.Common.Messaging;
using Domain.Entities;

namespace Application.CourseMaterials.GetCourseMaterialTest;

public record GetCourseMaterialTestQuery(Guid MaterialId) : IQuery<CourseMaterialTest>;
