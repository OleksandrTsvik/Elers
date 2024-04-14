import { Navigate, Outlet } from 'react-router-dom';

import useAuth from '../hooks/use-auth';
import useLocationFrom from '../hooks/use-location-from';

export default function AnonymousOutlet() {
  const redirectTo = useLocationFrom();
  const { isAuth } = useAuth();

  if (isAuth) {
    return <Navigate to={redirectTo} replace />;
  }

  return <Outlet />;
}
