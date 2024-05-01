import { RouteObject } from 'react-router-dom';

import {
  coursesRoutes,
  permissionsRoutes,
  rolesRoutes,
  usersRoutes,
} from './routes';

export const privateRoutes: RouteObject[] = [
  coursesRoutes,
  permissionsRoutes,
  rolesRoutes,
  usersRoutes,
];
