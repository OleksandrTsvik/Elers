import {
  NavigateOptions,
  To,
  useLocation,
  useNavigate,
} from 'react-router-dom';

import { getSearchParams, isObject } from '../utils/helpers';

export default function useNavigateFrom() {
  const location = useLocation();
  const navigate = useNavigate();

  return (to: To, options?: NavigateOptions) => {
    let state = {};
    location.search = getSearchParams().toString();

    if (isObject(options?.state)) {
      state = options.state;
    } else {
      state = { data: options?.state as unknown };
    }

    return navigate(to, { ...options, state: { ...state, from: location } });
  };
}
