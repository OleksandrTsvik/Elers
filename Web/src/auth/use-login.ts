import { LoginRequest, useLoginMutation } from './auth.api';
import { setCredentials } from './auth.slice';
import { sessionApiReducers } from '../api';
import { useAppDispatch } from '../hooks/redux-hooks';

export function useLogin() {
  const appDispatch = useAppDispatch();

  const [loginMutation, { isLoading, error }] = useLoginMutation();

  const login = async (data: LoginRequest) => {
    try {
      const response = await loginMutation(data).unwrap();

      appDispatch(setCredentials(response));

      sessionApiReducers.forEach((api) =>
        appDispatch(api.util.resetApiState()),
      );
    } catch (error) {
      /* empty */
    }
  };

  return { login, isLoading, error };
}
