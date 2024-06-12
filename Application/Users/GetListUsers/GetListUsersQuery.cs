using Application.Common.Messaging;
using Application.Common.Models;

namespace Application.Users.GetListUsers;

public record GetListUsersQuery(GetListUsersQueryParams QueryParams)
    : IQuery<PagedList<GetListUserItemResponse>>;
