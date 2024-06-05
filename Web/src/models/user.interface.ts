import { PermissionType } from '../auth';

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

export interface UserDto {
  id: string;
  firstName: string;
  lastName: string;
  patronymic: string;
}
