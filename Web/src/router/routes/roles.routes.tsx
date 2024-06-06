import { lazy } from 'react';

import { RoutesType } from './routes-type.interface';
import { PermissionType } from '../../auth';
import PermissionsOutlet from '../outlets/permissions.outlet';

const RoleCreationPage = lazy(
  () => import('../../pages/role-creation-page/role-creation.page'),
);

const RoleEditPage = lazy(
  () => import('../../pages/role-edit-page/role-edit.page'),
);

const RolesPage = lazy(() => import('../../pages/roles-page/roles.page'));

export const rolesRoutes: RoutesType = {
  private: {
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
        element: (
          <PermissionsOutlet permissions={[PermissionType.UpdateRole]} />
        ),
        children: [{ index: true, element: <RoleEditPage /> }],
      },
      {
        path: 'add',
        element: (
          <PermissionsOutlet permissions={[PermissionType.CreateRole]} />
        ),
        children: [{ index: true, element: <RoleCreationPage /> }],
      },
    ],
  },
  public: {},
};
