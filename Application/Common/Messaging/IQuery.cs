using Domain.Shared;
using MediatR;

namespace Application.Common.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
