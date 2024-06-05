using Application.Common.Messaging;
using Application.Common.Models;
using Application.Common.Queries;
using Application.Users.DTOs;
using Domain.Shared;

namespace Application.Assignments.GetListSubmittedAssignments;

public class GetListSubmittedAssignmentsQueryHandler
    : IQueryHandler<GetListSubmittedAssignmentsQuery, PagedList<SubmittedAssignmentListItem>>
{
    private readonly ICourseQueries _courseQueries;
    private readonly IAssignmentQueries _assignmentQueries;
    private readonly IUserQueries _userQueries;

    public GetListSubmittedAssignmentsQueryHandler(
        ICourseQueries courseQueries,
        IAssignmentQueries assignmentQueries,
        IUserQueries userQueries)
    {
        _courseQueries = courseQueries;
        _assignmentQueries = assignmentQueries;
        _userQueries = userQueries;
    }

    public async Task<Result<PagedList<SubmittedAssignmentListItem>>> Handle(
        GetListSubmittedAssignmentsQuery request,
        CancellationToken cancellationToken)
    {
        Guid[] tabIds = await _courseQueries.GetCourseTabIds(request.CourseId, cancellationToken);

        PagedList<SubmittedAssignmentListItemDto> submittedAssignments = await _assignmentQueries
            .GetListSubmittedAssignments(tabIds, request.QueryParams, cancellationToken);

        UserDto[] students = await _userQueries.GetUserDtosByIds(
            submittedAssignments.Items.Select(x => x.StudentId), cancellationToken);

        var response = new List<SubmittedAssignmentListItem>();

        foreach (SubmittedAssignmentListItemDto submittedAssignment in submittedAssignments.Items)
        {
            UserDto? student = students
                .Where(student => student.Id == submittedAssignment.StudentId)
                .FirstOrDefault();

            response.Add(new SubmittedAssignmentListItem
            {
                SubmittedAssignmentId = submittedAssignment.SubmittedAssignmentId,
                StudentId = submittedAssignment.StudentId,
                AssignmentId = submittedAssignment.AssignmentId,
                AssignmentTitle = submittedAssignment.AssignmentTitle,
                SubmittedDate = submittedAssignment.SubmittedDate,
                StudentFirstName = student?.FirstName,
                StudentLastName = student?.LastName,
                StudentPatronymic = student?.Patronymic,
            });
        }

        return new PagedList<SubmittedAssignmentListItem>(
            response,
            submittedAssignments.TotalCount,
            submittedAssignments.CurrentPage,
            submittedAssignments.PageSize);
    }
}
