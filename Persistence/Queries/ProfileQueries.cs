using Application.Common.Queries;
using Application.Profile.GetCurrentProfile;
using Application.Profile.GetMyEnrolledCourses;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Queries;

internal class ProfileQueries : IProfileQueries
{
    private readonly ApplicationDbContext _dbContext;

    public ProfileQueries(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<GetCurrentProfileResponse?> GetCurrentProfile(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.Users
            .Where(x => x.Id == userId)
            .Select(x => new GetCurrentProfileResponse
            {
                Email = x.Email,
                RegistrationDate = x.RegistrationDate,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Patronymic = x.Patronymic,
                AvatarUrl = x.AvatarUrl,
                BirthDate = x.BirthDate,
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<GetMyEnrolledCoursesResponse[]> GetMyEnrolledCourses(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.Courses
            .Where(x => x.CourseMembers.Any(courseMember => courseMember.UserId == userId))
            .OrderBy(x => x.Title)
            .Select(x => new GetMyEnrolledCoursesResponse
            {
                Id = x.Id,
                Title = x.Title
            })
            .ToArrayAsync(cancellationToken);
    }
}
