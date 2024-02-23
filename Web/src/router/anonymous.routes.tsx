import { RouteObject } from 'react-router-dom';

import { LoginPage } from '../pages';

export const anonymousRoutes: RouteObject[] = [
  { path: 'login', element: <LoginPage /> },
];
