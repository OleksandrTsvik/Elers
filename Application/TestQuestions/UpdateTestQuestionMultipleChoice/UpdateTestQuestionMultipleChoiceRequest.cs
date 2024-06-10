using Domain.Entities;

namespace Application.TestQuestions.UpdateTestQuestionMultipleChoice;

public record UpdateTestQuestionMultipleChoiceRequest(
    string Text,
    double Points,
    List<TestQuestionChoiceOption> Options);
