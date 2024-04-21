import { PermissionType } from '../models/permission-type.enum';

export default function hasPermission(
  userPermissions: PermissionType[],
  permissions: PermissionType | PermissionType[],
): boolean {
  const permissionsArr = Array.isArray(permissions)
    ? permissions
    : [permissions];

  if (permissionsArr.length === 0) {
    return true;
  }

  if (userPermissions.length === 0) {
    return false;
  }

  return permissionsArr.some((permission) =>
    userPermissions.includes(permission),
  );
}
