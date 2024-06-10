using Domain.Entities;

namespace Application.TestQuestions.CreateTestQuestionMultipleChoice;

public record CreateTestQuestionMultipleChoiceRequest(
    string Text,
    double Points,
    List<TestQuestionChoiceOption> Options);
