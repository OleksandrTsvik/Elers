using Domain.Entities;

namespace Application.TestQuestions.UpdateTestQuestionMatching;

public record UpdateTestQuestionMatchingRequest(
    string Text,
    double Points,
    List<TestQuestionMatchOption> Options);
