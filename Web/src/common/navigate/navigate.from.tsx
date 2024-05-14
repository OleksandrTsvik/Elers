import { Navigate, NavigateProps, useLocation } from 'react-router-dom';

import { getSearchParams, isObject } from '../../utils/helpers';

export default function NavigateFrom({ to, state, ...props }: NavigateProps) {
  const location = useLocation();
  location.search = getSearchParams().toString();

  return (
    <Navigate
      to={to}
      state={{
        ...(isObject(state) ? state : { data: state as unknown }),
        from: location,
      }}
      {...props}
    />
  );
}
