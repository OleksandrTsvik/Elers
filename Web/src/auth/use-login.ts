import { LoginRequest, useLoginMutation } from './auth.api';
import { setCredentials } from './auth.slice';
import { useAppDispatch } from '../hooks/redux-hooks';
import useDisplayError from '../hooks/use-display-error';

export default function useLogin() {
  const appDispatch = useAppDispatch();
  const { displayError } = useDisplayError();

  const [loginMutation, { isLoading, isError, error }] = useLoginMutation();

  const login = (data: LoginRequest) => {
    loginMutation(data)
      .unwrap()
      .then((response) => appDispatch(setCredentials(response)))
      .catch((error) => displayError(error, { display: false }));
  };

  return { login, isLoading, isError, error };
}
