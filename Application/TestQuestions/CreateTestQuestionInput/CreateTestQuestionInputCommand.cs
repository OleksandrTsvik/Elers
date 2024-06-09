using Application.Common.Messaging;

namespace Application.TestQuestions.CreateTestQuestionInput;

public record CreateTestQuestionInputCommand(
    Guid TestId,
    string Text,
    double Points,
    string Answer) : ICommand;
