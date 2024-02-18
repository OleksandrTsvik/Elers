import { RouteObject, createBrowserRouter } from 'react-router-dom';

import Test from './Test';
import LayoutPage from '../layout/layout.page';

const routes: RouteObject[] = [
  {
    path: '/',
    element: <LayoutPage />,
    children: [{ index: true, element: <Test /> }],
  },
];

export const router = createBrowserRouter(routes);
