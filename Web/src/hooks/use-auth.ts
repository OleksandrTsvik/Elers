import { useAppSelector } from './redux-hooks';
import { selectAuthState } from '../auth/auth.slice';

export default function useAuth() {
  const auth = useAppSelector(selectAuthState);

  return { ...auth };
}
