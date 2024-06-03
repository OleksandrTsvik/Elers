import { api } from '.';
import { RoleListItem, Role, UserRole } from '../models/role.interface';

interface UpdateRoleRequest {
  roleId: string;
  name: string;
  permissionIds: string[];
}

interface CreateRoleRequest {
  name: string;
  permissionIds: string[];
}

export const rolesApi = api.injectEndpoints({
  overrideExisting: false,
  endpoints: (builder) => ({
    getRoleById: builder.query<Role, { id: string }>({
      query: ({ id }) => ({
        url: `/roles/${id}`,
      }),
      providesTags: ['Session', 'Locale', 'Roles'],
    }),
    getListRoles: builder.query<RoleListItem[], void>({
      query: () => ({
        url: '/roles',
      }),
      providesTags: ['Session', 'Locale', 'Roles'],
    }),
    getListUserRoles: builder.query<UserRole[], void>({
      query: () => ({
        url: '/roles/users',
      }),
      providesTags: ['Session', 'Locale', 'Roles'],
    }),
    createRole: builder.mutation<void, CreateRoleRequest>({
      query: (data) => ({
        url: '/roles',
        method: 'POST',
        body: data,
      }),
      invalidatesTags: (_, error) => (error ? [] : ['Roles']),
    }),
    updateRole: builder.mutation<void, UpdateRoleRequest>({
      query: ({ roleId, ...data }) => ({
        url: `/roles/${roleId}`,
        method: 'PUT',
        body: data,
      }),
      invalidatesTags: (_, error) => (error ? [] : ['Roles']),
    }),
    deleteRole: builder.mutation<void, { id: string }>({
      query: ({ id }) => ({
        url: `/roles/${id}`,
        method: 'DELETE',
      }),
      invalidatesTags: (_, error) => (error ? [] : ['Roles']),
    }),
  }),
});

export const {
  useGetRoleByIdQuery,
  useGetListRolesQuery,
  useGetListUserRolesQuery,
  useCreateRoleMutation,
  useUpdateRoleMutation,
  useDeleteRoleMutation,
} = rolesApi;
