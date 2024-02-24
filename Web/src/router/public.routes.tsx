import { RouteObject } from 'react-router-dom';

import { HomePage } from '../pages';

export const publicRoutes: RouteObject[] = [
  { index: true, element: <HomePage /> },
  { path: '*', element: <>not-found</> },
];
