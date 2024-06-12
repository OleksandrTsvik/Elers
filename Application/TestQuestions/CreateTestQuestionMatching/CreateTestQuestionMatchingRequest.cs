using Domain.Entities;

namespace Application.TestQuestions.CreateTestQuestionMatching;

public record CreateTestQuestionMatchingRequest(
    string Text,
    double Points,
    List<TestQuestionMatchOption> Options);
