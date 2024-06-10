using Application.Common.Messaging;

namespace Application.Tests.GetTestSession;

public record GetTestSessionQuery(Guid TestSessionId) : IQuery<GetTestSessionResponse>;
