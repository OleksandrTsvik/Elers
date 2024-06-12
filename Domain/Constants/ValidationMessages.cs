namespace Domain.Constants;

public static class ValidationMessages
{
    public const string TrimWhitespace = "Remove unnecessary spaces at the beginning or end.";
    public const string IsUrl = "The link is invalid.";

    public static class TestQuestions
    {
        public const string AddTwoAnswers = "Add at least 2 answers.";
        public const string ChooseOneCorrectAnswer = "Choose one correct answer.";
        public const string AddThreeAnswers = "Add at least 3 answers.";
        public const string ChooseMoreThanOneCorrectAnswer = "Choose more than one correct answer.";
        public const string AddTwoMatching = "Add at least 2 questions.";
    }
}
