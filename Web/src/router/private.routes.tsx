import { RouteObject } from 'react-router-dom';

import { permissionsRoutes, rolesRoutes } from './routes';

export const privateRoutes: RouteObject[] = [permissionsRoutes, rolesRoutes];
