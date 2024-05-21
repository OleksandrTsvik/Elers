import { PermissionType } from '../auth/permission-type.enum';

export interface Permission {
  id: string;
  name: PermissionType;
  description: string;
}

export interface RolePermissionListItem {
  id: string;
  name: PermissionType;
  description: string;
  isSelected: boolean;
}
