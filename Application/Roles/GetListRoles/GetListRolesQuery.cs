using Application.Common.Messaging;
using Application.Common.Models;

namespace Application.Roles.GetListRoles;

public record GetListRolesQuery(GetListRolesQueryParams QueryParams)
    : IQuery<PagedList<GetListRoleItemResponse>>;
