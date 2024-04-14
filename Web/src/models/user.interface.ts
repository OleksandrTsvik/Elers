import { Permission } from './permission.enum';

export interface User {
  email: string;
  avatarUrl: string;
  permissions: Permission[];
}
