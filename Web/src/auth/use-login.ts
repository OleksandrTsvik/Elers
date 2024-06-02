import { LoginRequest, useLoginMutation } from './auth.api';
import { setCredentials } from './auth.slice';
import { useAppDispatch } from '../hooks/redux-hooks';

export function useLogin() {
  const appDispatch = useAppDispatch();

  const [loginMutation, { isLoading, error }] = useLoginMutation();

  const login = async (data: LoginRequest) => {
    await loginMutation(data)
      .unwrap()
      .then((response) => appDispatch(setCredentials(response)));
  };

  return { login, isLoading, error };
}
