using System.Text.Json.Serialization;
using Domain.Enums;
using Domain.Primitives;

namespace Domain.Entities;

[JsonDerivedType(typeof(TestQuestionInput))]
[JsonDerivedType(typeof(TestQuestionSingleChoice))]
[JsonDerivedType(typeof(TestQuestionMultipleChoice))]
public abstract class TestQuestion : Entity
{
    public Guid TestId { get; set; }
    public TestQuestionType Type { get; protected set; }
    public string Text { get; set; } = string.Empty;
    public double Points { get; set; }
}

public class TestQuestionInput : TestQuestion
{
    public string Answer { get; set; } = string.Empty;

    public TestQuestionInput() : base()
    {
        Type = TestQuestionType.Input;
    }
}

public class TestQuestionSingleChoice : TestQuestion
{
    public List<TestQuestionChoiceOption> Options { get; set; } = [];

    public TestQuestionSingleChoice() : base()
    {
        Type = TestQuestionType.SingleChoice;
    }
}

public class TestQuestionMultipleChoice : TestQuestion
{
    public List<TestQuestionChoiceOption> Options { get; set; } = [];

    public TestQuestionMultipleChoice() : base()
    {
        Type = TestQuestionType.MultipleChoice;
    }
}

public class TestQuestionChoiceOption
{
    public string Option { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
}
