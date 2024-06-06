import { lazy } from 'react';

import { RoutesType } from './routes-type.interface';
import { PermissionType } from '../../auth';
import PermissionsOutlet from '../outlets/permissions.outlet';

const PermissionsPage = lazy(
  () => import('../../pages/permissions-page/permissions.page'),
);

export const permissionsRoutes: RoutesType = {
  private: {
    path: 'permissions',
    element: (
      <PermissionsOutlet
        permissions={[PermissionType.ReadPermission, PermissionType.CreateRole]}
      />
    ),
    children: [{ index: true, element: <PermissionsPage /> }],
  },
  public: {},
};
