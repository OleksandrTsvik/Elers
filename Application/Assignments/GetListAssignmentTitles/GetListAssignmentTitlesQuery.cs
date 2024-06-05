using Application.Common.Messaging;

namespace Application.Assignments.GetListAssignmentTitles;

public record GetListAssignmentTitlesQuery(Guid CourseId) : IQuery<List<GetListAssignmentTitleItemResponse>>;
