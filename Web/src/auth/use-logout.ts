import { App } from 'antd';
import { useTranslation } from 'react-i18next';

import { logout as resetAuthState } from './auth.slice';
import { logoutApiReducers } from '../api';
import { useLogoutMutation } from '../api/account.api';
import { useAppDispatch } from '../hooks/redux-hooks';
import useDisplayError from '../hooks/use-display-error';

export default function useLogout() {
  const { t } = useTranslation();
  const appDispatch = useAppDispatch();

  const { message } = App.useApp();
  const { displayError } = useDisplayError();

  const [logoutMutation, { isLoading, error }] = useLogoutMutation();

  const logout = () => {
    const messageLoadingKey = 'logout';

    void message.loading({
      key: messageLoadingKey,
      content: t('loading.logout'),
      duration: 0,
    });

    logoutMutation()
      .unwrap()
      .then(() => {
        appDispatch(resetAuthState());

        logoutApiReducers.forEach((api) =>
          appDispatch(api.util.resetApiState()),
        );
      })
      .catch((error) => displayError(error))
      .finally(() => message.destroy(messageLoadingKey));
  };

  return { logout, isLoading, error };
}
