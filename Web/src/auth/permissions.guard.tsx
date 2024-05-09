import { PermissionType } from './permission-type.enum';
import useAuth from './use-auth';

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
