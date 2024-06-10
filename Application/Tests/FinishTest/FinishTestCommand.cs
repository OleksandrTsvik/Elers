using Application.Common.Messaging;

namespace Application.Tests.FinishTest;

public record FinishTestCommand(Guid TestSessionId) : ICommand;
