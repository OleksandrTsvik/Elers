import { createApi } from '@reduxjs/toolkit/query/react';

import { baseQueryWithReauth } from '../auth/base-query-with-reauth';
import { Permission } from '../models/permission.interface';

export const permissionsApi = createApi({
  reducerPath: 'permissionsApi',
  baseQuery: baseQueryWithReauth,
  endpoints: (builder) => ({
    getListPermissions: builder.query<Permission[], void>({
      query: () => ({
        url: '/permissions',
      }),
    }),
  }),
});

export const { useGetListPermissionsQuery } = permissionsApi;
