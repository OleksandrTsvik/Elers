using Application.Common.Messaging;
using Application.TestQuestions.DTOs;

namespace Application.TestQuestions.GetTestQuestionIdsAndTypes;

public record GetTestQuestionIdsAndTypesQuery(Guid TestId) : IQuery<List<TestQuestionIdsAndTypesDto>>;
