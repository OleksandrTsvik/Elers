using Application.Common.Messaging;
using Domain.Entities;

namespace Application.TestQuestions.CreateTestQuestionMatching;

public record CreateTestQuestionMatchingCommand(
    Guid TestId,
    string Text,
    double Points,
    List<TestQuestionMatchOption> Options) : ICommand;
