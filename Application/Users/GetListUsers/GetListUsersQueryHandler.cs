using Application.Common.Messaging;
using Application.Common.Queries;
using Domain.Shared;

namespace Application.Users.GetListUsers;

public class GetListUsersQueryHandler : IQueryHandler<GetListUsersQuery, GetListUserItemResponse[]>
{
    private readonly IUserQueries _userQueries;

    public GetListUsersQueryHandler(IUserQueries userQueries)
    {
        _userQueries = userQueries;
    }

    public async Task<Result<GetListUserItemResponse[]>> Handle(
        GetListUsersQuery request,
        CancellationToken cancellationToken)
    {
        return await _userQueries.GetListUsers(cancellationToken);
    }
}
