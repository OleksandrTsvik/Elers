import { selectAuthState } from './auth.slice';
import { hasPermission } from './has-permission.util';
import { PermissionType } from './permission-type.enum';
import { useAppSelector } from '../hooks/redux-hooks';

export function useAuth() {
  const auth = useAppSelector(selectAuthState);

  const checkPermission = (permissions: PermissionType | PermissionType[]) => {
    const userPermissions = auth.user?.permissions || [];

    return hasPermission(userPermissions, permissions);
  };

  return { ...auth, isAuth: !!auth.user, checkPermission };
}
