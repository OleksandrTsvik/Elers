using System.Text.Json.Serialization;
using Domain.Enums;
using Domain.Primitives;

namespace Domain.Entities;

public class TestSession : Entity
{
    public Guid TestId { get; set; }
    public Guid UserId { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? FinishedAt { get; set; }
    public List<TestSessionAnswer> Answers { get; set; } = [];

    public TestSession()
    {
        StartedAt = DateTime.UtcNow;
    }
}

[JsonDerivedType(typeof(TestSessionAnswerInput))]
[JsonDerivedType(typeof(TestSessionAnswerSingleChoice))]
[JsonDerivedType(typeof(TestSessionAnswerMultipleChoice))]
[JsonDerivedType(typeof(TestSessionAnswerMatching))]
public abstract class TestSessionAnswer
{
    public Guid QuestionId { get; set; }
    public TestQuestionType QuestionType { get; set; }
}

public class TestSessionAnswerInput : TestSessionAnswer
{
    public string? Answer { get; set; }

    public TestSessionAnswerInput() : base()
    {
        QuestionType = TestQuestionType.Input;
    }
}

public class TestSessionAnswerSingleChoice : TestSessionAnswer
{
    public string? Answer { get; set; }

    public TestSessionAnswerSingleChoice() : base()
    {
        QuestionType = TestQuestionType.SingleChoice;
    }
}

public class TestSessionAnswerMultipleChoice : TestSessionAnswer
{
    public List<string>? Answers { get; set; }

    public TestSessionAnswerMultipleChoice() : base()
    {
        QuestionType = TestQuestionType.MultipleChoice;
    }
}

public class TestSessionAnswerMatching : TestSessionAnswer
{
    public List<AnswerMatchOption>? MatchOptions { get; set; }

    public TestSessionAnswerMatching() : base()
    {
        QuestionType = TestQuestionType.Matching;
    }
}

public class AnswerMatchOption
{
    public string Question { get; set; } = string.Empty;
    public string? Answer { get; set; }
}
