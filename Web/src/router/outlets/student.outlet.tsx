import { Navigate, Outlet } from 'react-router-dom';

import { useAuth } from '../../auth';

export default function StudentOutlet() {
  const { isStudent } = useAuth();

  if (!isStudent) {
    return <Navigate to="/" replace />;
  }

  return <Outlet />;
}
