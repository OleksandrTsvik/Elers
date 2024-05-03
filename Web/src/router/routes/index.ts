import { coursesRoutes } from './courses.routes';
import { permissionsRoutes } from './permissions.routes';
import { rolesRoutes } from './roles.routes';
import { usersRoutes } from './users.routes';

const arrayRoutes = [
  coursesRoutes,
  permissionsRoutes,
  rolesRoutes,
  usersRoutes,
];

export const arrayPrivateRoutes = arrayRoutes.map((routes) => routes.private);

export const arrayPublicRoutes = arrayRoutes.map((routes) => routes.public);
