import { Navigate } from 'react-router-dom';

interface Props {
  error: unknown;
}

export default function NavigateToError({ error }: Props) {
  return <Navigate to="/error" replace state={{ error }} />;
}
