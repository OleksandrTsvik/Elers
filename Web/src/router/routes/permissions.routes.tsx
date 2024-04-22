import { RouteObject } from 'react-router-dom';

import { PermissionType } from '../../models/permission-type.enum';
import { PermissionsPage } from '../../pages';
import PermissionsOutlet from '../outlets/permissions.outlet';

export const permissionsRoutes: RouteObject = {
  path: 'permissions',
  element: (
    <PermissionsOutlet
      permissions={[PermissionType.ReadPermission, PermissionType.CreateRole]}
    />
  ),
  children: [{ index: true, element: <PermissionsPage /> }],
};
