import { Outlet } from 'react-router-dom';

import { useAuth } from '../../auth';
import { NavigateFrom } from '../../common/navigate';

export default function PrivateOutlet() {
  const { isAuth } = useAuth();

  if (!isAuth) {
    return <NavigateFrom to="/login" replace />;
  }

  return <Outlet />;
}
