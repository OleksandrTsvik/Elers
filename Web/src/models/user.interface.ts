import { PermissionType } from '../auth/permission-type.enum';

export interface AuthUser {
  email: string;
  avatarUrl: string;
  permissions: PermissionType[];
}

export interface User {
  id: string;
  firstName: string;
  lastName: string;
  patronymic: string;
  email: string;
  roles: string[];
}
