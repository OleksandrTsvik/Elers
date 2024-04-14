import { RouteObject } from 'react-router-dom';

import PermissionsOutlet from './permissions.outlet';
import { Permission } from '../models/permission.enum';

export const privateRoutes: RouteObject[] = [
  { path: 'private', element: <>private</> },
  {
    path: 'permissions',
    element: <PermissionsOutlet permissions={Permission.CreateUser} />,
    children: [],
  },
];
