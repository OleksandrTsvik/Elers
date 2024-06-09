using Application.Common.Messaging;

namespace Application.TestQuestions.GetTestQuestionIdsAndTypes;

public record GetTestQuestionIdsAndTypesQuery(Guid TestId)
    : IQuery<List<GetTestQuestionIdsAndTypesResponse>>;
