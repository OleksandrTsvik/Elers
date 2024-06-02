import { CoursePermissionType } from './course-permission-type.enum';

export function hasCoursePermission(
  memberPermissions: CoursePermissionType[],
  permissions: CoursePermissionType | CoursePermissionType[],
): boolean {
  const permissionsArr = Array.isArray(permissions)
    ? permissions
    : [permissions];

  if (permissionsArr.length === 0) {
    return true;
  }

  if (memberPermissions.length === 0) {
    return false;
  }

  return permissionsArr.some((permission) =>
    memberPermissions.includes(permission),
  );
}
