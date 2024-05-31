using Application.Common.Queries;
using Application.CourseMembers.GetListCourseMembers;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Queries;

public class CourseMemberQueries : ICourseMemberQueries
{
    private readonly ApplicationDbContext _dbContext;

    public CourseMemberQueries(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<GetListCourseMemberItemResponse[]> GetListCourseMembers(
        Guid courseId,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.CourseMembers
            .Where(x => x.CourseId == courseId && x.User != null)
            .OrderBy(x => x.User!.FirstName)
                .ThenBy(x => x.User!.LastName)
                .ThenBy(x => x.User!.Patronymic)
            .Select(courseMember => new GetListCourseMemberItemResponse
            {
                Id = courseMember.User!.Id,
                FirstName = courseMember.User.FirstName,
                LastName = courseMember.User.LastName,
                Patronymic = courseMember.User.Patronymic,
                AvatarUrl = courseMember.User.AvatarUrl,
            })
            .ToArrayAsync(cancellationToken);
    }

    public Task<GetListCourseMemberItemResponse[]> GetListCourseMembersWithRoles(
        Guid courseId,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.CourseMembers
            .Where(x => x.CourseId == courseId && x.User != null)
            .OrderBy(x => x.User!.FirstName)
                .ThenBy(x => x.User!.LastName)
                .ThenBy(x => x.User!.Patronymic)
            .Select(courseMember => new GetListCourseMemberItemResponse
            {
                Id = courseMember.User!.Id,
                FirstName = courseMember.User.FirstName,
                LastName = courseMember.User.LastName,
                Patronymic = courseMember.User.Patronymic,
                AvatarUrl = courseMember.User.AvatarUrl,
                CourseRole = new GetListCourseMemberItemRoleResponse
                {
                    Id = courseMember.CourseRole != null ? courseMember.CourseRole.Id : null,
                    Description = courseMember.CourseRole != null ? courseMember.CourseRole.Name : null
                }
            })
            .ToArrayAsync(cancellationToken);
    }
}
