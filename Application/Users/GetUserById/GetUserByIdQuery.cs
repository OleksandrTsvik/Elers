using Application.Common.Messaging;

namespace Application.Users.GetUserById;

public record GetUserByIdQuery(Guid UserId) : IQuery<GetUserByIdResponse>;
