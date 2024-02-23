import { useCallback } from 'react';

import { LoginRequest, useLoginMutation } from './auth.api';
import { setCredentials } from './auth.slice';
import { useAppDispatch } from '../hooks/redux-hooks';
import useDisplayError from '../hooks/use-display-error';

export default function useLogin() {
  const appDispatch = useAppDispatch();
  const { displayError } = useDisplayError();

  const [loginMutation, { isLoading, isError, error }] = useLoginMutation();

  const login = useCallback(
    (data: LoginRequest) => {
      loginMutation(data)
        .unwrap()
        .then((response) => appDispatch(setCredentials(response)))
        .catch((error) => displayError(error, { display: false }));
    },
    [loginMutation, appDispatch, displayError],
  );

  return { login, isLoading, isError, error };
}
