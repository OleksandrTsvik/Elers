import { Navigate, Outlet } from 'react-router-dom';

import useAuth from '../../auth/use-auth';
import useLocationFrom from '../../hooks/use-location-from';

export default function AnonymousOutlet() {
  const { locationFrom: redirectTo } = useLocationFrom();
  const { isAuth } = useAuth();

  if (isAuth) {
    return <Navigate to={redirectTo} replace />;
  }

  return <Outlet />;
}
