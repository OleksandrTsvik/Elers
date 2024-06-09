namespace Application.TestQuestions.CreateTestQuestionInput;

public record CreateTestQuestionInputRequest(
    string Text,
    double Points,
    string Answer);
