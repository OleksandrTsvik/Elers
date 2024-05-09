import { RoutesType } from './routes-type.interface';
import { PermissionType } from '../../auth/permission-type.enum';
import { RoleCreationPage, RoleEditPage, RolesPage } from '../../pages';
import PermissionsOutlet from '../outlets/permissions.outlet';

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
