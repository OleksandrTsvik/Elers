using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.GetListUsers;

public class GetListUsersQueryHandler : IQueryHandler<GetListUsersQuery, GetListUserItemResponse[]>
{
    private readonly IApplicationDbContext _context;

    public GetListUsersQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<GetListUserItemResponse[]>> Handle(
        GetListUsersQuery request,
        CancellationToken cancellationToken)
    {
        GetListUserItemResponse[] users = await _context.Users
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
            .ToArrayAsync(cancellationToken);

        return users;
    }
}
