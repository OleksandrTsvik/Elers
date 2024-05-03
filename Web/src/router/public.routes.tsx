import { RouteObject } from 'react-router-dom';

import { coursesRoutes } from './routes';
import { HomePage, NotFoundPage } from '../pages';

export const publicRoutes: RouteObject[] = [
  { index: true, element: <HomePage /> },
  coursesRoutes.public,
  { path: '*', element: <NotFoundPage /> },
];
