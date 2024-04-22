import { accountApi } from './account.api';
import { permissionsApi } from './permissions.api';
import { rolesApi } from './roles.api';
import { authApi } from '../auth/auth.api';

export const apiReducers = [authApi, accountApi, permissionsApi, rolesApi];

// state of reducers will be reset after logout
export const logoutApiReducers = [accountApi, permissionsApi, rolesApi];

// state of reducers will be reset after changing the language
export const localizedApiReducers = [permissionsApi, rolesApi];
