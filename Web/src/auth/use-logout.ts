import { App } from 'antd';
import { useTranslation } from 'react-i18next';

import { useLogoutMutation } from './auth.api';
import { logout as resetAuthState } from './auth.slice';
import { useAppDispatch } from '../hooks/redux-hooks';
import useDisplayError from '../hooks/use-display-error';

const messageLoadingKey = 'logout';

export function useLogout() {
  const { t } = useTranslation();
  const appDispatch = useAppDispatch();

  const { message } = App.useApp();
  const { displayError } = useDisplayError();

  const [logoutMutation, { isLoading, error }] = useLogoutMutation();

  const logout = async () => {
    void message.loading({
      key: messageLoadingKey,
      content: t('loading.logout'),
      duration: 0,
    });

    await logoutMutation()
      .unwrap()
      .then(() => appDispatch(resetAuthState()))
      .catch((error) => displayError(error))
      .finally(() => message.destroy(messageLoadingKey));
  };

  return { logout, isLoading, error };
}
