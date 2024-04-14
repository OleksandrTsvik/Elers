import { Spin } from 'antd';
import { useLayoutEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';

import { useRefreshMutation } from './auth.api';
import { setCredentials } from './auth.slice';
import { useAppDispatch } from '../hooks/redux-hooks';
import useAuth from '../hooks/use-auth';
import useDisplayError from '../hooks/use-display-error';

interface Props {
  children: React.ReactNode;
}

export default function AuthMiddleware({ children }: Props) {
  const { t } = useTranslation();
  const appDispatch = useAppDispatch();
  const { displayError } = useDisplayError();

  const { isAuth } = useAuth();
  const [refresh] = useRefreshMutation();

  const [isLoading, setIsLoading] = useState(true);

  useLayoutEffect(() => {
    setTimeout(() => {
      if (isAuth) {
        setIsLoading(false);
        return;
      }

      refresh()
        .unwrap()
        .then((response) => appDispatch(setCredentials(response)))
        .catch((error) => displayError(error, { display: false }))
        .finally(() => setIsLoading(false));
    }, 1200);

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  if (isLoading) {
    return <Spin fullscreen tip={t('loading.app')} />;
  }

  return <>{children}</>;
}
