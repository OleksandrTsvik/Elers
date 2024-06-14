using Application.Profile.GetCurrentProfile;
using Application.Profile.GetMyEnrolledCourses;

namespace Application.Common.Queries;

public interface IProfileQueries
{
    Task<GetCurrentProfileResponse?> GetCurrentProfile(
        Guid userId,
        CancellationToken cancellationToken = default);

    Task<GetMyEnrolledCoursesResponse[]> GetMyEnrolledCourses(
        Guid userId,
        CancellationToken cancellationToken = default);
}
