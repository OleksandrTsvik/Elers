import { Location, useLocation } from 'react-router-dom';

export type LocationFrom = string | Partial<Location>;
export type LocationStateFrom = { from: LocationFrom } | undefined;

export default function useLocationFrom(defaultLocation: LocationFrom = '/') {
  const location = useLocation();

  const locationFrom = (location.state as LocationStateFrom)?.from;

  return {
    locationFrom: locationFrom ?? defaultLocation,
    hasFromLocation: !!locationFrom,
  };
}
