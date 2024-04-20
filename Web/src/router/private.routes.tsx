import { RouteObject } from 'react-router-dom';

import PermissionsOutlet from './permissions.outlet';
import { Permission } from '../models/permission.enum';
import { RolesPage } from '../pages';

export const privateRoutes: RouteObject[] = [
  { path: 'courses', element: <>courses</> },
  {
    path: 'roles',
    element: (
      <PermissionsOutlet
        permissions={[
          Permission.CreateRole,
          Permission.ReadRole,
          Permission.UpdateRole,
          Permission.UpdateRolePermissions,
          Permission.DeleteRole,
        ]}
      />
    ),
    children: [{ index: true, element: <RolesPage /> }],
  },
];
