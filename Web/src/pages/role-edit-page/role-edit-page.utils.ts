import { Key } from 'antd/es/table/interface';

import { FormValues } from './role-edit.form';
import { RolePermissions } from '../../models/permission.interface';
import { Role } from '../../models/role.interface';

export function getInitialValues(data: Role): FormValues {
  return {
    name: data.name,
    permissionIds: data.permissions
      .filter((item) => item.isSelected)
      .map((item) => item.id),
  };
}

export function getDefaultSelectedRowKeys(data: RolePermissions[]): Key[] {
  return data.filter((item) => item.isSelected).map((item) => item.id);
}
