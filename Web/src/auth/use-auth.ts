import { selectAuthState } from './auth.slice';
import { hasPermission } from './has-permission.util';
import { PermissionType } from './permission-type.enum';
import { AuthItem, AuthItemMenu, MenuItem } from '../common/types';
import { useAppSelector } from '../hooks/redux-hooks';
import { UserType } from '../models/user.interface';

export function useAuth() {
  const auth = useAppSelector(selectAuthState);

  const checkPermission = (permissions: PermissionType | PermissionType[]) => {
    const userPermissions = auth.user?.permissions || [];

    return hasPermission(userPermissions, permissions);
  };

  const filterItems = <T extends AuthItem, U>(items: T[]): U[] =>
    items
      .filter(
        (item) =>
          item.show ??
          (item.check || checkPermission(item.userPermissions ?? [])),
      )
      .map(
        // eslint-disable-next-line @typescript-eslint/no-unused-vars
        ({ userPermissions, show, check, ...item }) => item as U,
      );

  const filterMenu = (items: AuthItemMenu[]): MenuItem[] => filterItems(items);

  return {
    ...auth,
    isAuth: !!auth.user,
    isStudent: auth.user?.type === UserType.Student,
    isTeacher: auth.user?.type === UserType.Teacher,
    checkPermission,
    filterMenu,
  };
}
