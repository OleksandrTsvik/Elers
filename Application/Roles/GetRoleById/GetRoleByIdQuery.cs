using Application.Common.Messaging;

namespace Application.Roles.GetRoleById;

public record GetRoleByIdQuery(Guid RoleId) : IQuery<GetRoleByIdResponse>;
