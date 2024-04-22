import { Role } from '../../models/role.interface';
import { RoleFormValues } from '../../shared';

export function getInitialValues(data: Role): RoleFormValues {
  return {
    name: data.name,
    permissionIds: data.permissions
      .filter((item) => item.isSelected)
      .map((item) => item.id),
  };
}
