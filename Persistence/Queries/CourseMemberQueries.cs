using System.Linq.Expressions;
using Application.Common.Models;
using Application.Common.Queries;
using Application.CourseMembers.GetListCourseMembers;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Extensions;

namespace Persistence.Queries;

public class CourseMemberQueries : ICourseMemberQueries
{
    private readonly ApplicationDbContext _dbContext;

    public CourseMemberQueries(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<PagedList<CourseMemberListItem>> GetListCourseMembers(
        Guid courseId,
        bool isGetWithRoles,
        GetListCourseMembersQueryParams queryParams,
        CancellationToken cancellationToken = default)
    {
        Expression<Func<CourseMember, object>> keySelector = queryParams.SortColumn?.ToLower() switch
        {
            "firstname" => x => x.User!.FirstName,
            "lastname" => x => x.User!.LastName,
            "patronymic" => x => x.User!.Patronymic,
            _ => x => x.Id
        };

        IQueryable<CourseMember> courseMembersQuery = _dbContext.CourseMembers
            .Where(x => x.CourseId == courseId && x.User != null)
            .SortBy(queryParams.SortOrder, keySelector);

        if (!string.IsNullOrWhiteSpace(queryParams.FirstName))
        {
            courseMembersQuery = courseMembersQuery
                .Where(x => EF.Functions.ILike(x.User!.FirstName, $"%{queryParams.FirstName}%"));
        }

        if (!string.IsNullOrWhiteSpace(queryParams.LastName))
        {
            courseMembersQuery = courseMembersQuery
                .Where(x => EF.Functions.ILike(x.User!.LastName, $"%{queryParams.LastName}%"));
        }

        if (!string.IsNullOrWhiteSpace(queryParams.Patronymic))
        {
            courseMembersQuery = courseMembersQuery
                .Where(x => EF.Functions.ILike(x.User!.Patronymic, $"%{queryParams.Patronymic}%"));
        }

        IQueryable<CourseMemberListItem> query;

        if (isGetWithRoles)
        {
            query = courseMembersQuery.Select(courseMember => new CourseMemberListItem
            {
                Id = courseMember.User!.Id,
                FirstName = courseMember.User.FirstName,
                LastName = courseMember.User.LastName,
                Patronymic = courseMember.User.Patronymic,
                AvatarUrl = courseMember.User.AvatarUrl,
                CourseRole = new CourseMemberListItemRole
                {
                    Id = courseMember.CourseRole != null ? courseMember.CourseRole.Id : null,
                    Description = courseMember.CourseRole != null ? courseMember.CourseRole.Name : null
                }
            });
        }
        else
        {
            query = courseMembersQuery.Select(courseMember => new CourseMemberListItem
            {
                Id = courseMember.User!.Id,
                FirstName = courseMember.User.FirstName,
                LastName = courseMember.User.LastName,
                Patronymic = courseMember.User.Patronymic,
                AvatarUrl = courseMember.User.AvatarUrl,
            });
        }

        return query.ToPagedListAsync(queryParams.PageNumber, queryParams.PageSize, cancellationToken);
    }
}
