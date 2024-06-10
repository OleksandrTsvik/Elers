using Domain.Enums;

namespace Application.TestQuestions.DTOs;

public class TestQuestionIdsAndTypesDto
{
    public required Guid Id { get; init; }
    public required TestQuestionType Type { get; init; }
}
