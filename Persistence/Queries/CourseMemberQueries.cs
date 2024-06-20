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
            "patronymic" => x => x.User!.Patronymic,
            _ => x => x.User!.LastName
        };

        IQueryable<CourseMember> courseMembersQuery = _dbContext.CourseMembers
            .Where(x => x.CourseId == courseId && x.User != null)
            .SortBy(queryParams.SortOrder, keySelector)
            .WhereIf(
                !string.IsNullOrWhiteSpace(queryParams.FirstName),
                x => EF.Functions.ILike(x.User!.FirstName, $"%{queryParams.FirstName}%"))
            .WhereIf(
                !string.IsNullOrWhiteSpace(queryParams.LastName),
                x => EF.Functions.ILike(x.User!.LastName, $"%{queryParams.LastName}%"))
            .WhereIf(
                !string.IsNullOrWhiteSpace(queryParams.Patronymic),
                x => EF.Functions.ILike(x.User!.Patronymic, $"%{queryParams.Patronymic}%"))
            .WhereIf(
                queryParams.Roles is not null,
                x => x.CourseRole != null && queryParams.Roles!.Contains(x.CourseRole.Id))
            .WhereIf(queryParams.UserTypes is not null, x => queryParams.UserTypes!.Contains(x.User!.Type));

        IQueryable<CourseMemberListItem> query;

        if (isGetWithRoles)
        {
            query = courseMembersQuery.Select(courseMember => new CourseMemberListItem
            {
                Id = courseMember.Id,
                UserId = courseMember.User!.Id,
                UserType = courseMember.User.Type,
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
                Id = courseMember.Id,
                UserId = courseMember.User!.Id,
                FirstName = courseMember.User.FirstName,
                LastName = courseMember.User.LastName,
                Patronymic = courseMember.User.Patronymic,
                AvatarUrl = courseMember.User.AvatarUrl,
            });
        }

        return query.ToPagedListAsync(queryParams.PageNumber, queryParams.PageSize, cancellationToken);
    }
}
