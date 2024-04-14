import hasPermission from './has-permission.util';
import useAuth from '../hooks/use-auth';
import { Permission } from '../models/permission.enum';

interface Props {
  permissions: Permission | Permission[];
  children: React.ReactNode;
}

export default function PermissionsGuard({ permissions, children }: Props) {
  const { user } = useAuth();

  if (!user || !hasPermission(user.permissions, permissions)) {
    return null;
  }

  return <>{children}</>;
}
