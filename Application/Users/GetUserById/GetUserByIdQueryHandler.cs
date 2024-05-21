using Application.Common.Messaging;
using Application.Common.Queries;
using Domain.Errors;
using Domain.Shared;

namespace Application.Users.GetUserById;

public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, GetUserByIdResponse>
{
    private readonly IUserQueries _userQueries;

    public GetUserByIdQueryHandler(IUserQueries userQueries)
    {
        _userQueries = userQueries;
    }

    public async Task<Result<GetUserByIdResponse>> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        GetUserByIdResponse? user = await _userQueries.GetUserById(request.UserId, cancellationToken);

        if (user is null)
        {
            return UserErrors.NotFound(request.UserId);
        }

        return user;
    }
}
