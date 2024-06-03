import { RolePermissionListItem } from './permission.interface';

export interface Role {
  id: string;
  name: string;
  permissions: RolePermissionListItem[];
}

export interface RoleListItem {
  id: string;
  name: string;
  permissions: string[];
}

export interface UserRole {
  id: string;
  name: string;
}
