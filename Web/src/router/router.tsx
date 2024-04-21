import { RouteObject, createBrowserRouter } from 'react-router-dom';

import { anonymousRoutes } from './anonymous.routes';
import AnonymousOutlet from './outlets/anonymous.outlet';
import PrivateOutlet from './outlets/private.outlet';
import { privateRoutes } from './private.routes';
import { publicRoutes } from './public.routes';
import LayoutPage from '../layout/layout.page';

const routes: RouteObject[] = [
  {
    path: '/',
    element: <LayoutPage />,
    children: [
      {
        element: <PrivateOutlet />,
        children: privateRoutes,
      },
      {
        element: <AnonymousOutlet />,
        children: anonymousRoutes,
      },
      ...publicRoutes,
    ],
  },
];

export const router = createBrowserRouter(routes, {
  basename: import.meta.env.BASE_URL,
});
