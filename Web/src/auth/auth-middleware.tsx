import { Spin } from 'antd';
import { useLayoutEffect, useState } from 'react';

import { setCredentials } from './auth.slice';
import { useAppDispatch } from '../hooks/store';
import useAuth from '../hooks/use-auth';

interface Props {
  children: React.ReactNode;
}

export default function AuthMiddleware({ children }: Props) {
  const appDispatch = useAppDispatch();

  const [isLoading, setIsLoading] = useState(true);
  const { user } = useAuth();

  useLayoutEffect(() => {
    setTimeout(() => {
      if (user) {
        setIsLoading(false);
        return;
      }

      appDispatch(setCredentials({ user: 'test' }));
    }, 1200);
  }, [user, appDispatch]);

  if (isLoading) {
    return <Spin fullscreen tip="Loading app..." />;
  }

  return <>{children}</>;
}
