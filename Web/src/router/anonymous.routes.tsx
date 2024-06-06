import { lazy } from 'react';
import { RouteObject } from 'react-router-dom';

const LoginPage = lazy(() => import('../pages/login-page/login.page'));

export const anonymousRoutes: RouteObject[] = [
  { path: 'login', element: <LoginPage /> },
];
