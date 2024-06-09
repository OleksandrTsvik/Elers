using Application.Common.Messaging;

namespace Application.TestQuestions.UpdateTestQuestionInput;

public record UpdateTestQuestionInputCommand(
    Guid TestQuestionId,
    string Text,
    double Points,
    string Answer) : ICommand;
