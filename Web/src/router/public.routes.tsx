import { RouteObject } from 'react-router-dom';

import { arrayPublicRoutes } from './routes';
import { HomePage, NotFoundPage } from '../pages';

export const publicRoutes: RouteObject[] = [
  { index: true, element: <HomePage /> },
  ...arrayPublicRoutes,
  { path: '*', element: <NotFoundPage /> },
];
