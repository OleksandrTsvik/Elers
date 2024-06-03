using Application.Common.Messaging;
using Domain.Entities;

namespace Application.CourseMaterials.GetCourseMaterialAssignment;

public record GetCourseMaterialAssignmentQuery(Guid MaterialId) : IQuery<CourseMaterialAssignment>;
