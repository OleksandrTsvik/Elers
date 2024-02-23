import { RouteObject, createBrowserRouter } from 'react-router-dom';

import AnonymousOutlet from './anonymous.outlet';
import { anonymousRoutes } from './anonymous.routes';
import PrivateOutlet from './private.outlet';
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

export const router = createBrowserRouter(routes);
