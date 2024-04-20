import { useAppSelector } from './redux-hooks';
import { selectAuthState } from '../auth/auth.slice';
import hasPermission from '../auth/has-permission.util';
import { Permission } from '../models/permission.enum';

export default function useAuth() {
  const auth = useAppSelector(selectAuthState);

  const checkPermission = (permissions: Permission | Permission[]) => {
    const userPermissions = auth.user?.permissions || [];

    return hasPermission(userPermissions, permissions);
  };

  return { ...auth, isAuth: !!auth.user, checkPermission };
}
