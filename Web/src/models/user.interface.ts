import { PermissionType } from '../auth';

export interface AuthUser {
  type: UserType;
  email: string;
  avatarUrl?: string;
  permissions: PermissionType[];
}

export interface User {
  id: string;
  type: UserType;
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
  avatarUrl?: string;
}

export enum UserType {
  User = 'User',
  Student = 'Student',
  Teacher = 'Teacher',
}
