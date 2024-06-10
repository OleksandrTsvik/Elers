using Domain.Entities;

namespace Application.TestQuestions.UpdateTestQuestionSingleChoice;

public record UpdateTestQuestionSingleChoiceRequest(
    string Text,
    double Points,
    List<TestQuestionChoiceOption> Options);
