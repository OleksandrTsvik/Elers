import { colorModeSlice } from './color-mode.slice';
import { localeSlice } from './locale.slice';
import { accountApi } from '../api/account.api';
import { coursePermissionsApi } from '../api/course-permissions.api';
import { courseRolesApi } from '../api/course-roles.api';
import { coursesApi } from '../api/courses.api';
import { permissionsApi } from '../api/permissions.api';
import { rolesApi } from '../api/roles.api';
import { usersApi } from '../api/users.api';
import { authApi, authSlice } from '../auth';
import { courseEditSlice } from '../pages/course-edit-page/course-edit.slice';

const rootReducer = {
  [authSlice.name]: authSlice.reducer,
  [colorModeSlice.name]: colorModeSlice.reducer,
  [localeSlice.name]: localeSlice.reducer,
  [courseEditSlice.name]: courseEditSlice.reducer,
  [authApi.reducerPath]: authApi.reducer,
  [accountApi.reducerPath]: accountApi.reducer,
  [coursePermissionsApi.reducerPath]: coursePermissionsApi.reducer,
  [courseRolesApi.reducerPath]: courseRolesApi.reducer,
  [coursesApi.reducerPath]: coursesApi.reducer,
  [permissionsApi.reducerPath]: permissionsApi.reducer,
  [rolesApi.reducerPath]: rolesApi.reducer,
  [usersApi.reducerPath]: usersApi.reducer,
};

export default rootReducer;
