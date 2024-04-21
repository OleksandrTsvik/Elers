import { PermissionType } from './permission-type.enum';

export interface User {
  email: string;
  avatarUrl: string;
  permissions: PermissionType[];
}
