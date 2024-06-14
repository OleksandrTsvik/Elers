using System.Linq.Expressions;
using Application.Common.Models;
using Application.Common.Queries;
using Application.Users.DTOs;
using Application.Users.GetListUsers;
using Application.Users.GetUserById;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Extensions;

namespace Persistence.Queries;

internal class UserQueries : IUserQueries
{
    private readonly ApplicationDbContext _dbContext;

    public UserQueries(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<PagedList<GetListUserItemResponse>> GetListUsers(
        GetListUsersQueryParams queryParams,
        CancellationToken cancellationToken = default)
    {
        Expression<Func<User, object>> keySelector = queryParams.SortColumn?.ToLower() switch
        {
            "firstname" => x => x.FirstName,
            "patronymic" => x => x.Patronymic,
            "email" => x => x.Email,
            _ => x => x.LastName
        };

        IQueryable<User> query = _dbContext.Users
            .SortBy(queryParams.SortOrder, keySelector)
            .WhereIf(
                !string.IsNullOrWhiteSpace(queryParams.FirstName),
                x => EF.Functions.ILike(x.FirstName, $"%{queryParams.FirstName}%"))
            .WhereIf(
                !string.IsNullOrWhiteSpace(queryParams.LastName),
                x => EF.Functions.ILike(x.LastName, $"%{queryParams.LastName}%"))
            .WhereIf(
                !string.IsNullOrWhiteSpace(queryParams.Patronymic),
                x => EF.Functions.ILike(x.Patronymic, $"%{queryParams.Patronymic}%"))
            .WhereIf(
                !string.IsNullOrWhiteSpace(queryParams.Email),
                x => EF.Functions.ILike(x.Email, $"%{queryParams.Email}%"))
            .WhereIf(queryParams.Types is not null, x => queryParams.Types!.Contains(x.Type))
            .WhereIf(
                queryParams.Roles is not null,
                x => queryParams.Roles!.Any(queryRole => x.Roles.Any(userRole => userRole.Id == queryRole)));

        return query
            .Select(x => new GetListUserItemResponse
            {
                Id = x.Id,
                Type = x.Type,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Patronymic = x.Patronymic,
                Email = x.Email,
                Roles = x.Roles.Select(role => role.Name).ToArray()
            })
            .ToPagedListAsync(queryParams.PageNumber, queryParams.PageSize, cancellationToken);
    }

    public Task<GetUserByIdResponse?> GetUserById(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Users
            .Select(x => new GetUserByIdResponse
            {
                Id = x.Id,
                Type = x.Type,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Patronymic = x.Patronymic,
                Email = x.Email,
                Roles = x.Roles.Select(role => role.Name).ToArray()
            })
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task<UserDto?> GetUserDtoById(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Users
            .Select(x => new UserDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Patronymic = x.Patronymic,
                AvatarUrl = x.AvatarUrl
            })
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task<UserDto[]> GetUserDtosByIds(
        IEnumerable<Guid> userIds,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.Users
            .Where(x => userIds.Contains(x.Id))
            .Select(x => new UserDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Patronymic = x.Patronymic,
                AvatarUrl = x.AvatarUrl
            })
            .ToArrayAsync(cancellationToken);
    }
}
