import { Spin } from 'antd';
import { useLayoutEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';

import { useRefreshMutation } from './auth.api';
import { setCredentials } from './auth.slice';
import { useAuth } from './use-auth';
import { useAppDispatch } from '../hooks/redux-hooks';
import useDisplayError from '../hooks/use-display-error';

interface Props {
  children: React.ReactNode;
}

export function AuthMiddleware({ children }: Props) {
  const { t } = useTranslation();
  const appDispatch = useAppDispatch();
  const { displayError } = useDisplayError();

  const { isAuth } = useAuth();
  const [refresh] = useRefreshMutation();

  const [isLoading, setIsLoading] = useState(true);

  useLayoutEffect(() => {
    if (isAuth) {
      setIsLoading(false);
      return;
    }

    refresh()
      .unwrap()
      .then((response) => appDispatch(setCredentials(response)))
      .catch((error) => displayError(error, { display: false }))
      .finally(() => setIsLoading(false));

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  if (isLoading) {
    return <Spin fullscreen tip={t('loading.app')} />;
  }

  return children;
}
