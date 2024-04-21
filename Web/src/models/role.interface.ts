import { RolePermissions } from './permission.interface';

export interface Role {
  id: string;
  name: string;
  permissions: RolePermissions[];
}

export interface ListRoleItem {
  id: string;
  name: string;
  permissions: string[];
}
