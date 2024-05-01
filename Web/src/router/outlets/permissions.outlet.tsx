import { useEffect } from 'react';
import { useTranslation } from 'react-i18next';
import { Navigate, Outlet, useLocation } from 'react-router-dom';

import useAuth from '../../hooks/use-auth';
import useDisplayError from '../../hooks/use-display-error';
import useLocationFrom from '../../hooks/use-location-from';
import { PermissionType } from '../../models/permission-type.enum';

interface Props {
  permissions: PermissionType | PermissionType[];
}

export default function PermissionsOutlet({ permissions }: Props) {
  const { t } = useTranslation();
  const location = useLocation();

  const redirectTo = useLocationFrom();
  const { isAuth, checkPermission } = useAuth();

  const { displayError } = useDisplayError();

  const hasPermission = checkPermission(permissions);

  useEffect(() => {
    if (!hasPermission) {
      displayError(t('error.forbidden'), { displayType: 'notification' });
    }
  }, [displayError, hasPermission, t]);

  if (!isAuth) {
    return <Navigate to="/login" replace state={{ from: location }} />;
  }

  if (!hasPermission) {
    return <Navigate to={redirectTo} replace />;
  }

  return <Outlet />;
}
