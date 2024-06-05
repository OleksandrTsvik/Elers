using Application.Auth.GetInfo;
using Application.Common.Queries;
using Application.Users.DTOs;
using Application.Users.GetListUsers;
using Application.Users.GetUserById;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Queries;

internal class UserQueries : IUserQueries
{
    private readonly ApplicationDbContext _dbContext;

    public UserQueries(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<GetInfoResponse?> GetInfo(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Users
            .Where(x => x.Id == id)
            .Select(x => new GetInfoResponse
            {
                Email = x.Email,
                RegistrationDate = x.RegistrationDate,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Patronymic = x.Patronymic,
                AvatarUrl = x.AvatarUrl,
                BirthDate = x.BirthDate
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<GetListUserItemResponse[]> GetListUsers(CancellationToken cancellationToken = default)
    {
        return _dbContext.Users
            .Select(x => new GetListUserItemResponse
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Patronymic = x.Patronymic,
                Email = x.Email,
                Roles = x.Roles.Select(role => role.Name).ToArray()
            })
            .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ThenBy(x => x.Patronymic)
                .ThenBy(x => x.Email)
            .ToArrayAsync(cancellationToken);
    }

    public Task<GetUserByIdResponse?> GetUserById(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Users
            .Select(x => new GetUserByIdResponse
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Patronymic = x.Patronymic,
                Email = x.Email,
                Roles = x.Roles.Select(role => role.Name).ToArray()
            })
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task<TeacherDto?> GetTeacherDtoById(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Users
            .Select(x => new TeacherDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Patronymic = x.Patronymic
            })
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
