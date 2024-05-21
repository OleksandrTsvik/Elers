using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Queries;
using Domain.Errors;
using Domain.Shared;

namespace Application.Auth.GetInfo;

public class GetInfoQueryHandler : IQueryHandler<GetInfoQuery, GetInfoResponse>
{
    private readonly IUserQueries _userQueries;
    private readonly IUserContext _userContext;

    public GetInfoQueryHandler(IUserQueries userQueries, IUserContext userContext)
    {
        _userQueries = userQueries;
        _userContext = userContext;
    }

    public async Task<Result<GetInfoResponse>> Handle(GetInfoQuery request, CancellationToken cancellationToken)
    {
        GetInfoResponse? user = await _userQueries.GetInfo(_userContext.UserId, cancellationToken);

        if (user is null)
        {
            return UserErrors.NotFoundByUserContext();
        }

        return user;
    }
}
