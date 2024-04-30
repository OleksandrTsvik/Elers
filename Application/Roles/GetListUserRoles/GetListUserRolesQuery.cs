using Application.Common.Messaging;

namespace Application.Roles.GetListUserRoles;

public record GetListUserRolesQuery() : IQuery<GetListUserRoleItemResponse[]>;
