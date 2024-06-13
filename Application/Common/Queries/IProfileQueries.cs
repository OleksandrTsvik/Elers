using Application.Profile.GetMyEnrolledCourses;

namespace Application.Common.Queries;

public interface IProfileQueries
{
    Task<GetMyEnrolledCoursesResponse[]> GetMyEnrolledCourses(
        Guid userId,
        CancellationToken cancellationToken = default);
}
