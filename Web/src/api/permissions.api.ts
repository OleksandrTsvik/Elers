import { api } from '.';
import { Permission } from '../models/permission.interface';

export const permissionsApi = api.injectEndpoints({
  overrideExisting: false,
  endpoints: (builder) => ({
    getListPermissions: builder.query<Permission[], void>({
      query: () => ({
        url: '/permissions',
      }),
      providesTags: ['Session', 'Locale'],
    }),
  }),
});

export const { useGetListPermissionsQuery } = permissionsApi;
