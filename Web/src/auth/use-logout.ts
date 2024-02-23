import { App } from 'antd';
import { useCallback } from 'react';
import { useTranslation } from 'react-i18next';

import { useLogoutMutation } from './auth.api';
import { logout as resetAuthState } from './auth.slice';
import { useAppDispatch } from '../hooks/redux-hooks';
import useDisplayError from '../hooks/use-display-error';

export default function useLogout() {
  const { t } = useTranslation();
  const appDispatch = useAppDispatch();

  const { message } = App.useApp();
  const { displayError } = useDisplayError();

  const [logoutMutation, { isLoading, isError, error }] = useLogoutMutation();

  const logout = useCallback(() => {
    const messageLoadingKey = 'logout';

    void message.loading({
      key: messageLoadingKey,
      content: t('loading_logout'),
      duration: 0,
    });

    logoutMutation()
      .unwrap()
      .catch((error) => {
        message.destroy(messageLoadingKey);
        displayError(error);
      })
      .finally(() => {
        appDispatch(resetAuthState());
        message.destroy(messageLoadingKey);
      });
  }, [message, t, logoutMutation, displayError, appDispatch]);

  return { logout, isLoading, isError, error };
}
