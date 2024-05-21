import { RolePermissionListItem } from './permission.interface';
import { PermissionType } from '../auth/permission-type.enum';

export interface Role {
  id: string;
  name: string;
  permissions: RolePermissionListItem[];
}

export interface RoleListItem {
  id: string;
  name: string;
  permissions: PermissionType[];
}

export interface UserRole {
  id: string;
  name: string;
}
