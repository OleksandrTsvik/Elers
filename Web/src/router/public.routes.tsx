import { lazy } from 'react';
import { RouteObject } from 'react-router-dom';

import { arrayPublicRoutes } from './routes';

const ErrorPage = lazy(() => import('../pages/error-page/error.page'));
const HomePage = lazy(() => import('../pages/home-page/home.page'));

const NotFoundPage = lazy(
  () => import('../pages/not-found-page/not-found.page'),
);

export const publicRoutes: RouteObject[] = [
  { index: true, element: <HomePage /> },
  ...arrayPublicRoutes,
  { path: '/error', element: <ErrorPage /> },
  { path: '*', element: <NotFoundPage /> },
];
