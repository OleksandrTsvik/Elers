import { fetchBaseQuery } from '@reduxjs/toolkit/query/react';

import { RootState } from '../store';
import { REACT_APP_API_URL } from '../utils/constants/node-env.constants';

export const getBaseQuery = (url: string = '') =>
  fetchBaseQuery({
    baseUrl: REACT_APP_API_URL + url,
    credentials: 'include',
    prepareHeaders: (headers, { getState }) => {
      const state = getState() as RootState;

      headers.set('Accept-Language', state.localeSlice.locale);

      return headers;
    },
  });
