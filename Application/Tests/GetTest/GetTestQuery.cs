using Application.Common.Messaging;

namespace Application.Tests.GetTest;

public record GetTestQuery(Guid TestId) : IQuery<GetTestResponse>;
