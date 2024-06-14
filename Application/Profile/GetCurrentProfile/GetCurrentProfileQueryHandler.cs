using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Queries;
using Domain.Errors;
using Domain.Shared;

namespace Application.Profile.GetCurrentProfile;

public class GetCurrentProfileQueryHandler : IQueryHandler<GetCurrentProfileQuery, GetCurrentProfileResponse>
{
    private readonly IProfileQueries _profileQueries;
    private readonly IUserContext _userContext;

    public GetCurrentProfileQueryHandler(IProfileQueries profileQueries, IUserContext userContext)
    {
        _profileQueries = profileQueries;
        _userContext = userContext;
    }

    public async Task<Result<GetCurrentProfileResponse>> Handle(
        GetCurrentProfileQuery request,
        CancellationToken cancellationToken)
    {
        GetCurrentProfileResponse? currentProfile = await _profileQueries.GetCurrentProfile(
            _userContext.UserId, cancellationToken);

        if (currentProfile is null)
        {
            return UserErrors.NotFound(_userContext.UserId);
        }

        return currentProfile;
    }
}
