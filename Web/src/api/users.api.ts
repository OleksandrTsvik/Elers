import { createApi } from '@reduxjs/toolkit/query/react';

import { baseQueryWithReauth } from '../auth/base-query-with-reauth';
import { User } from '../models/user.interface';

export const usersApi = createApi({
  reducerPath: 'usersApi',
  baseQuery: baseQueryWithReauth,
  endpoints: (builder) => ({
    getListUsers: builder.query<User[], void>({
      query: () => ({
        url: '/users',
      }),
    }),
  }),
});

export const { useGetListUsersQuery } = usersApi;
