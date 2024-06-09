namespace Application.TestQuestions.UpdateTestQuestionInput;

public record UpdateTestQuestionInputRequest(
    string Text,
    double Points,
    string Answer);
