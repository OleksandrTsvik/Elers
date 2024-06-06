import { lazy } from 'react';

import { RoutesType } from './routes-type.interface';
import { PermissionType } from '../../auth';
import PermissionsOutlet from '../outlets/permissions.outlet';

const UserCreationPage = lazy(
  () => import('../../pages/user-creation-page/user-creation.page'),
);

const UserEditPage = lazy(
  () => import('../../pages/user-edit-page/user-edit.page'),
);

const UsersPage = lazy(() => import('../../pages/users-page/users.page'));

export const usersRoutes: RoutesType = {
  private: {
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
        path: 'edit/:userId',
        element: <PermissionsOutlet permissions={PermissionType.UpdateUser} />,
        children: [{ index: true, element: <UserEditPage /> }],
      },
      {
        path: 'add',
        element: <PermissionsOutlet permissions={PermissionType.CreateUser} />,
        children: [{ index: true, element: <UserCreationPage /> }],
      },
    ],
  },
  public: {},
};
