using Domain.Enums;

namespace Application.TestQuestions.GetTestQuestionIdsAndTypes;

public class GetTestQuestionIdsAndTypesResponse
{
    public required Guid Id { get; init; }
    public required TestQuestionType Type { get; init; }
}
