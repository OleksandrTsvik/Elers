import { RouteObject } from 'react-router-dom';

import { CoursesPage } from '../../pages';

export const coursesRoutes: RouteObject = {
  path: 'courses',
  children: [{ index: true, element: <CoursesPage /> }],
};
