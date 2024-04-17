using Application.Common.Messaging;

namespace Application.Roles.GetListRoles;

public record GetListRolesQuery() : IQuery<GetListRoleItemResponse[]>;
