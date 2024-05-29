import { createApi } from '@reduxjs/toolkit/query/react';

import { baseQueryWithReauth } from '../auth';

export const accountApi = createApi({
  reducerPath: 'accountApi',
  baseQuery: baseQueryWithReauth,
  endpoints: (builder) => ({
    logout: builder.mutation<void, void>({
      query: () => ({
        url: '/auth/logout',
        method: 'POST',
        body: {},
      }),
    }),
  }),
});

export const { useLogoutMutation } = accountApi;
