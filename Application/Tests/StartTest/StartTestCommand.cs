using Application.Common.Messaging;

namespace Application.Tests.StartTest;

public record StartTestCommand(Guid TestId) : ICommand<Guid>;
