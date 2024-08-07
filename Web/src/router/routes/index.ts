import { courseMaterialsRoutes } from './course-materials.routes';
import { courseMenuRoutes } from './course-menu.routes';
import { coursesRoutes } from './courses.routes';
import { permissionsRoutes } from './permissions.routes';
import { profileRoutes } from './profile.routes';
import { rolesRoutes } from './roles.routes';
import { usersRoutes } from './users.routes';

const arrayRoutes = [
  courseMaterialsRoutes,
  coursesRoutes,
  permissionsRoutes,
  profileRoutes,
  rolesRoutes,
  usersRoutes,
];

export const arrayPrivateRoutes = arrayRoutes.map((routes) => routes.private);

export const arrayPublicRoutes = arrayRoutes.map((routes) => routes.public);

export { courseMenuRoutes };
