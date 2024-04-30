import { createApi } from '@reduxjs/toolkit/query/react';

import { baseQueryWithReauth } from '../auth/base-query-with-reauth';
import { User } from '../models/user.interface';

interface CreateUserRequest {
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  patronymic: string;
  roleIds: string[];
}

export const usersApi = createApi({
  reducerPath: 'usersApi',
  baseQuery: baseQueryWithReauth,
  tagTypes: ['Users'],
  endpoints: (builder) => ({
    getListUsers: builder.query<User[], void>({
      query: () => ({
        url: '/users',
      }),
      providesTags: ['Users'],
    }),
    createUser: builder.mutation<void, CreateUserRequest>({
      query: (data) => ({
        url: '/users',
        method: 'POST',
        body: data,
      }),
      invalidatesTags: ['Users'],
    }),
  }),
});

export const { useGetListUsersQuery, useCreateUserMutation } = usersApi;
