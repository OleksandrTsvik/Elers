using System.Text.Json.Serialization;
using Domain.Enums;

namespace Application.Tests.GetTestSessionQuestion;

[JsonDerivedType(typeof(GetTestSessionQuestionInputResponse))]
[JsonDerivedType(typeof(GetTestSessionQuestionSingleChoiceResponse))]
[JsonDerivedType(typeof(GetTestSessionQuestionMultipleChoiceResponse))]
public class GetTestSessionQuestionResponse
{
    public required Guid QuestionId { get; init; }
    public required TestQuestionType QuestionType { get; init; }
    public required string QuestionText { get; init; }
    public required double Points { get; init; }
}

public class GetTestSessionQuestionInputResponse : GetTestSessionQuestionResponse
{
    public required string? UserAnswer { get; init; }
}

public class GetTestSessionQuestionSingleChoiceResponse : GetTestSessionQuestionResponse
{
    public required string[] Options { get; init; }
    public required string? UserAnswer { get; init; }
}

public class GetTestSessionQuestionMultipleChoiceResponse : GetTestSessionQuestionResponse
{
    public required string[] Options { get; init; }
    public required List<string>? UserAnswers { get; init; }
}
