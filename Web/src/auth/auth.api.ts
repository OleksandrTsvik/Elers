import { createApi } from '@reduxjs/toolkit/query/react';

import { getBaseQuery } from '../api/get-base-query';
import { User } from '../models/user.interface';

export interface LoginRequest {
  email: string;
  password: string;
}

export const authApi = createApi({
  reducerPath: 'authApi',
  baseQuery: getBaseQuery('/auth'),
  endpoints: (builder) => ({
    login: builder.mutation<User, LoginRequest>({
      query: (data) => ({
        url: '/login',
        method: 'POST',
        body: data,
      }),
    }),
    refresh: builder.mutation<User, void>({
      query: () => ({
        url: '/refresh',
        method: 'PUT',
        body: {},
      }),
    }),
  }),
});

export const { useLoginMutation, useRefreshMutation } = authApi;
