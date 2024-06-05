using Application.Common.Messaging;
using Application.Common.Queries;
using Application.Users.DTOs;
using Domain.Shared;

namespace Application.Students.GetCourseStudents;

public class GetCourseStudentsQueryHandler : IQueryHandler<GetCourseStudentsQuery, UserDto[]>
{
    private readonly IStudentQueries _studentQueries;

    public GetCourseStudentsQueryHandler(IStudentQueries studentQueries)
    {
        _studentQueries = studentQueries;
    }

    public async Task<Result<UserDto[]>> Handle(
        GetCourseStudentsQuery request,
        CancellationToken cancellationToken)
    {
        return await _studentQueries.GetCourseStudents(request.CourseId, cancellationToken);
    }
}
