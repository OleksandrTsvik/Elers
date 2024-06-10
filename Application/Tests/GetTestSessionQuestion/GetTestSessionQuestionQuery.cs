using Application.Common.Messaging;

namespace Application.Tests.GetTestSessionQuestion;

public record GetTestSessionQuestionQuery(
    Guid TestSessionId,
    Guid TestQuestionId) : IQuery<GetTestSessionQuestionResponse>;
