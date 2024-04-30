import { RouteObject } from 'react-router-dom';

import { PermissionType } from '../../models/permission-type.enum';
import { UserCreationPage, UsersPage } from '../../pages';
import PermissionsOutlet from '../outlets/permissions.outlet';

export const usersRoutes: RouteObject = {
  path: 'users',
  element: (
    <PermissionsOutlet
      permissions={[
        PermissionType.CreateUser,
        PermissionType.ReadUser,
        PermissionType.UpdateUser,
        PermissionType.DeleteUser,
      ]}
    />
  ),
  children: [
    { index: true, element: <UsersPage /> },
    {
      path: 'add',
      element: <PermissionsOutlet permissions={PermissionType.CreateUser} />,
      children: [{ index: true, element: <UserCreationPage /> }],
    },
  ],
};
