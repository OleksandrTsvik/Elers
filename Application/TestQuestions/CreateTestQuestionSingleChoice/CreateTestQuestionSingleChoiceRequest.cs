using Domain.Entities;

namespace Application.TestQuestions.CreateTestQuestionSingleChoice;

public record CreateTestQuestionSingleChoiceRequest(
    string Text,
    double Points,
    List<TestQuestionChoiceOption> Options);
