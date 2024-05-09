import { Navigate, Outlet, useLocation } from 'react-router-dom';

import useAuth from '../../auth/use-auth';

export default function PrivateOutlet() {
  const location = useLocation();
  const { isAuth } = useAuth();

  if (!isAuth) {
    return <Navigate to="/login" replace state={{ from: location }} />;
  }

  return <Outlet />;
}
