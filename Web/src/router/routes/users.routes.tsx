import { RoutesType } from './routes-type.interface';
import { PermissionType } from '../../auth/permission-type.enum';
import { UserCreationPage, UserEditPage, UsersPage } from '../../pages';
import PermissionsOutlet from '../outlets/permissions.outlet';

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
