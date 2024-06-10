using Application.Common.Messaging;
using Domain.Entities;

namespace Application.TestQuestions.UpdateTestQuestionSingleChoice;

public record UpdateTestQuestionSingleChoiceCommand(
    Guid TestQuestionId,
    string Text,
    double Points,
    List<TestQuestionChoiceOption> Options) : ICommand;
