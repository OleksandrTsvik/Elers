import { useEffect } from 'react';
import { useTranslation } from 'react-i18next';
import { Navigate, Outlet } from 'react-router-dom';

import { PermissionType, useAuth } from '../../auth';
import { NavigateFrom } from '../../common/navigate';
import useDisplayError from '../../hooks/use-display-error';
import useLocationFrom from '../../hooks/use-location-from';

interface Props {
  permissions: PermissionType | PermissionType[];
}

export default function PermissionsOutlet({ permissions }: Props) {
  const { t } = useTranslation();

  const { locationFrom: redirectTo } = useLocationFrom();
  const { isAuth, checkPermission } = useAuth();

  const { displayError } = useDisplayError();

  const hasPermission = checkPermission(permissions);

  useEffect(() => {
    if (!hasPermission) {
      displayError(t('error.forbidden'), { displayType: 'notification' });
    }
  }, [displayError, hasPermission, t]);

  if (!isAuth) {
    return <NavigateFrom to="/login" replace />;
  }

  if (!hasPermission) {
    return <Navigate to={redirectTo} replace />;
  }

  return <Outlet />;
}
