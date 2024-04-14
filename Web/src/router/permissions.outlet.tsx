import { useEffect } from 'react';
import { useTranslation } from 'react-i18next';
import { Navigate, Outlet, useLocation } from 'react-router-dom';

import hasPermission from '../auth/has-permission.util';
import useAuth from '../hooks/use-auth';
import useDisplayError from '../hooks/use-display-error';
import useLocationFrom from '../hooks/use-location-from';
import { Permission } from '../models/permission.enum';

interface Props {
  permissions: Permission | Permission[];
}

export default function PermissionsOutlet({ permissions }: Props) {
  const { t } = useTranslation();
  const location = useLocation();

  const redirectTo = useLocationFrom();
  const { user } = useAuth();

  const { displayError } = useDisplayError();

  useEffect(() => {
    if (user && !hasPermission(user.permissions, permissions)) {
      displayError(t('error.access_denied'), { displayType: 'notification' });
    }
  }, [displayError, permissions, t, user]);

  if (!user) {
    return <Navigate to="/login" replace state={{ from: location }} />;
  }

  if (!hasPermission(user.permissions, permissions)) {
    return <Navigate to={redirectTo} replace />;
  }

  return (
    <>
      <Outlet />
    </>
  );
}
