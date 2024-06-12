using Application.Common.Messaging;
using Application.Common.Models;
using Application.Common.Queries;
using Domain.Shared;

namespace Application.Users.GetListUsers;

public class GetListUsersQueryHandler : IQueryHandler<GetListUsersQuery, PagedList<GetListUserItemResponse>>
{
    private readonly IUserQueries _userQueries;

    public GetListUsersQueryHandler(IUserQueries userQueries)
    {
        _userQueries = userQueries;
    }

    public async Task<Result<PagedList<GetListUserItemResponse>>> Handle(
        GetListUsersQuery request,
        CancellationToken cancellationToken)
    {
        return await _userQueries.GetListUsers(request.QueryParams, cancellationToken);
    }
}
