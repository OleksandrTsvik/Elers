import { Permission } from '../models/permission.enum';

export default function hasPermission(
  userPermissions: Permission[],
  permissions: Permission | Permission[],
): boolean {
  const permissionsArr = Array.isArray(permissions)
    ? permissions
    : [permissions];

  if (permissionsArr.length === 0) {
    return true;
  }

  return permissionsArr.some((permission) =>
    userPermissions.includes(permission),
  );
}
