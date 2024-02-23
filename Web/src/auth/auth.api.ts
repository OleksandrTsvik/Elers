import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

import { User } from '../models/user.interface';
import { REACT_APP_API_URL } from '../utils/constants/node-env.constants';

export interface LoginRequest {
  email: string;
  password: string;
}

const baseQuery = fetchBaseQuery({
  baseUrl: REACT_APP_API_URL + '/auth',
  credentials: 'include',
});

export const authApi = createApi({
  reducerPath: 'authApi',
  baseQuery,
  endpoints: (builder) => ({
    login: builder.mutation<User, LoginRequest>({
      query: (data) => ({
        url: '/login',
        method: 'POST',
        body: data,
      }),
    }),
    logout: builder.mutation<void, void>({
      query: () => ({
        url: '/logout',
        method: 'POST',
        body: {},
      }),
    }),
    refresh: builder.mutation<User, void>({
      query: () => ({
        url: '/refresh',
        method: 'POST',
        body: {},
      }),
    }),
  }),
});

export const { useLoginMutation, useLogoutMutation, useRefreshMutation } =
  authApi;
