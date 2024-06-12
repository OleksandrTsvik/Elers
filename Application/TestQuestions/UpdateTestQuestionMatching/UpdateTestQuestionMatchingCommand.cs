using Application.Common.Messaging;
using Domain.Entities;

namespace Application.TestQuestions.UpdateTestQuestionMatching;

public record UpdateTestQuestionMatchingCommand(
    Guid TestQuestionId,
    string Text,
    double Points,
    List<TestQuestionMatchOption> Options) : ICommand;
