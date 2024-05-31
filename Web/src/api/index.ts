import { accountApi } from './account.api';
import { courseMembersApi } from './course-members.mutations.api';
import { coursePermissionsApi } from './course-permissions.api';
import { courseRolesApi } from './course-roles.api';
import { coursesApi } from './courses.api';
import { permissionsApi } from './permissions.api';
import { rolesApi } from './roles.api';
import { usersApi } from './users.api';
import { authApi } from '../auth';

export const apiReducers = [
  authApi,
  accountApi,
  courseMembersApi,
  coursePermissionsApi,
  courseRolesApi,
  coursesApi,
  permissionsApi,
  rolesApi,
  usersApi,
];

// state of reducers will be reset after login or logout
export const sessionApiReducers = [
  accountApi,
  coursePermissionsApi,
  coursesApi,
  permissionsApi,
  rolesApi,
  usersApi,
];

// state of reducers will be reset after changing the language
export const localizedApiReducers = [
  coursePermissionsApi,
  courseRolesApi,
  permissionsApi,
  rolesApi,
];
