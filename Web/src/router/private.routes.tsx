import { RouteObject } from 'react-router-dom';

import { permissionsRoutes, rolesRoutes, usersRoutes } from './routes';

export const privateRoutes: RouteObject[] = [
  permissionsRoutes,
  rolesRoutes,
  usersRoutes,
];
