using Application.Common.Messaging;

namespace Application.Users.GetListUsers;

public record GetListUsersQuery() : IQuery<GetListUserItemResponse[]>;
