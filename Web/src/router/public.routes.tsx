import { RouteObject } from 'react-router-dom';

import { HomePage, NotFoundPage } from '../pages';

export const publicRoutes: RouteObject[] = [
  { index: true, element: <HomePage /> },
  { path: '*', element: <NotFoundPage /> },
];
