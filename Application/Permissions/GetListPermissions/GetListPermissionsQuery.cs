using Application.Common.Messaging;

namespace Application.Permissions.GetListPermissions;

public record GetListPermissionsQuery() : IQuery<GetListPermissionItemResponse[]>;
