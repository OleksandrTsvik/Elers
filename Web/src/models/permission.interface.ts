import { PermissionType } from '../auth';

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
