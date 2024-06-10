using Application.Common.Messaging;
using Domain.Entities;

namespace Application.TestQuestions.CreateTestQuestionMultipleChoice;

public record CreateTestQuestionMultipleChoiceCommand(
    Guid TestId,
    string Text,
    double Points,
    List<TestQuestionChoiceOption> Options) : ICommand;
