using Application.Common.Messaging;

namespace Application.Profile.GetCurrentProfile;

public record GetCurrentProfileQuery() : IQuery<GetCurrentProfileResponse>;
