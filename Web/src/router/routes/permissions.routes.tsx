import { RoutesType } from './routes-type.interface';
import { PermissionType } from '../../auth';
import { PermissionsPage } from '../../pages';
import PermissionsOutlet from '../outlets/permissions.outlet';

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
