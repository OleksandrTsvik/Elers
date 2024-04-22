import { createApi } from '@reduxjs/toolkit/query/react';

import { baseQueryWithReauth } from '../auth/base-query-with-reauth';
import { ListRoleItem, Role } from '../models/role.interface';

interface UpdateRoleRequest {
  roleId: string;
  name: string;
  permissionIds: string[];
}

interface CreateRoleRequest {
  name: string;
  permissionIds: string[];
}

export const rolesApi = createApi({
  reducerPath: 'rolesApi',
  baseQuery: baseQueryWithReauth,
  tagTypes: ['Roles'],
  endpoints: (builder) => ({
    getRoleById: builder.query<Role, { id: string }>({
      query: ({ id }) => ({
        url: `/roles/${id}`,
      }),
      providesTags: ['Roles'],
    }),
    getListRoles: builder.query<ListRoleItem[], void>({
      query: () => ({
        url: '/roles',
      }),
      providesTags: ['Roles'],
    }),
    createRole: builder.mutation<void, CreateRoleRequest>({
      query: (data) => ({
        url: '/roles',
        method: 'POST',
        body: data,
      }),
      invalidatesTags: ['Roles'],
    }),
    updateRole: builder.mutation<void, UpdateRoleRequest>({
      query: ({ roleId, ...data }) => ({
        url: `/roles/${roleId}`,
        method: 'PUT',
        body: data,
      }),
      invalidatesTags: ['Roles'],
    }),
    deleteRole: builder.mutation<void, { id: string }>({
      query: ({ id }) => ({
        url: `/roles/${id}`,
        method: 'DELETE',
      }),
      invalidatesTags: ['Roles'],
    }),
  }),
});

export const {
  useGetRoleByIdQuery,
  useGetListRolesQuery,
  useCreateRoleMutation,
  useUpdateRoleMutation,
  useDeleteRoleMutation,
} = rolesApi;
