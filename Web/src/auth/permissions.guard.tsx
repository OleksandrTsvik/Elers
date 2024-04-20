import useAuth from '../hooks/use-auth';
import { Permission } from '../models/permission.enum';

interface Props {
  permissions: Permission | Permission[];
  children: React.ReactNode;
}

export default function PermissionsGuard({ permissions, children }: Props) {
  const { checkPermission } = useAuth();

  if (!checkPermission(permissions)) {
    return null;
  }

  return <>{children}</>;
}
