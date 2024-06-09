using Application.Common.Messaging;
using Domain.Entities;

namespace Application.TestQuestions.GetTestQuestion;

public record GetTestQuestionQuery(Guid TestQuestionId) : IQuery<TestQuestion>;
