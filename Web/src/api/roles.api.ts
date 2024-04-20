import { createApi } from '@reduxjs/toolkit/query/react';

import { baseQueryWithReauth } from '../auth/base-query-with-reauth';
import { ListRoleItem } from '../models/role.interface';

export const rolesApi = createApi({
  reducerPath: 'rolesApi',
  baseQuery: baseQueryWithReauth,
  endpoints: (builder) => ({
    getListRoles: builder.query<ListRoleItem[], void>({
      query: () => ({
        url: '/roles',
      }),
    }),
  }),
});

export const { useGetListRolesQuery } = rolesApi;
