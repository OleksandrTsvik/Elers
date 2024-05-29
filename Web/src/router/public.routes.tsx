import { RouteObject } from 'react-router-dom';

import { arrayPublicRoutes } from './routes';
import { ErrorPage, HomePage, NotFoundPage } from '../pages';

export const publicRoutes: RouteObject[] = [
  { index: true, element: <HomePage /> },
  ...arrayPublicRoutes,
  { path: '/error', element: <ErrorPage /> },
  { path: '*', element: <NotFoundPage /> },
];
