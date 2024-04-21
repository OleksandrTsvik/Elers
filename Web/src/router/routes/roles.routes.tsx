import { RouteObject } from 'react-router-dom';

import { PermissionType } from '../../models/permission-type.enum';
import { RoleEditPage, RolesPage } from '../../pages';
import PermissionsOutlet from '../outlets/permissions.outlet';

export const rolesRoutes: RouteObject = {
  path: 'roles',
  element: (
    <PermissionsOutlet
      permissions={[
        PermissionType.CreateRole,
        PermissionType.ReadRole,
        PermissionType.UpdateRole,
        PermissionType.DeleteRole,
      ]}
    />
  ),
  children: [
    { index: true, element: <RolesPage /> },
    {
      path: 'edit/:roleId',
      element: <PermissionsOutlet permissions={[PermissionType.UpdateRole]} />,
      children: [{ index: true, element: <RoleEditPage /> }],
    },
  ],
};
