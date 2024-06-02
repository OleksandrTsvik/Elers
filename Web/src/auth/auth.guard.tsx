import { useAuth } from './use-auth';

interface Props {
  children: React.ReactNode;
}

export function AuthGuard({ children }: Props) {
  const { isAuth } = useAuth();

  return isAuth ? children : null;
}
