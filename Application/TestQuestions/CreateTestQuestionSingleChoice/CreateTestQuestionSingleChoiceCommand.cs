using Application.Common.Messaging;
using Domain.Entities;

namespace Application.TestQuestions.CreateTestQuestionSingleChoice;

public record CreateTestQuestionSingleChoiceCommand(
    Guid TestId,
    string Text,
    double Points,
    List<TestQuestionChoiceOption> Options) : ICommand;
