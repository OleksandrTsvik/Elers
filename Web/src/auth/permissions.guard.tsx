import useAuth from '../hooks/use-auth';
import { PermissionType } from '../models/permission-type.enum';

interface Props {
  permissions: PermissionType | PermissionType[];
  children: React.ReactNode;
}

export default function PermissionsGuard({ permissions, children }: Props) {
  const { checkPermission } = useAuth();

  if (!checkPermission(permissions)) {
    return null;
  }

  return <>{children}</>;
}
