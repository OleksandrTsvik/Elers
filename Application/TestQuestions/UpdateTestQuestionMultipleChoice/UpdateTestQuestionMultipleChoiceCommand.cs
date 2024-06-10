using Application.Common.Messaging;
using Domain.Entities;

namespace Application.TestQuestions.UpdateTestQuestionMultipleChoice;

public record UpdateTestQuestionMultipleChoiceCommand(
    Guid TestQuestionId,
    string Text,
    double Points,
    List<TestQuestionChoiceOption> Options) : ICommand;
