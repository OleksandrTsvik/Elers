import { Location, useLocation } from 'react-router-dom';

type LocationState = { from: Location } | null;

export default function useLocationFrom(
  defaultLocation: string | Partial<Location> = '/',
) {
  const location = useLocation();

  return (location.state as LocationState)?.from || defaultLocation;
}
