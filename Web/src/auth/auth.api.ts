import { api } from '../api';
import { AuthUser } from '../models/user.interface';

export interface LoginRequest {
  email: string;
  password: string;
}

export const authApi = api.injectEndpoints({
  overrideExisting: false,
  endpoints: (builder) => ({
    login: builder.mutation<AuthUser, LoginRequest>({
      query: (data) => ({
        url: '/auth/login',
        method: 'POST',
        body: data,
      }),
      invalidatesTags: ['Session'],
    }),
    refresh: builder.mutation<AuthUser, void>({
      query: () => ({
        url: '/auth/refresh',
        method: 'PUT',
        body: {},
      }),
    }),
  }),
});

export const { useLoginMutation, useRefreshMutation } = authApi;
