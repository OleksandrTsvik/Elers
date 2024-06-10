using Application.Common.Messaging;

namespace Application.TestQuestions.DeleteTestQuestion;

public record DeleteTestQuestionCommand(Guid TestQuestionId) : ICommand;
