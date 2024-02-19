import { Spin } from 'antd';
import { useLayoutEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';

import { setCredentials } from './auth.slice';
import { useAppDispatch } from '../hooks/redux-hooks';
import useAuth from '../hooks/use-auth';

interface Props {
  children: React.ReactNode;
}

export default function AuthMiddleware({ children }: Props) {
  const { t } = useTranslation();
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
    return <Spin fullscreen tip={t('loading_app')} />;
  }

  return <>{children}</>;
}
