import { api } from '.';
import { PagedList, PagingParams, SortParams } from '../common/types';
import { User, UserType } from '../models/user.interface';

interface GetListUsersRequest extends PagingParams, SortParams {
  firstName?: string;
  lastName?: string;
  patronymic?: string;
  email?: string;
  roles?: string[];
  types?: string[];
}

interface CreateUserRequest {
  type: UserType;
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  patronymic: string;
  roleIds: string[];
}

interface UpdateUserRequest {
  userId: string;
  type: UserType;
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  patronymic: string;
  roleIds: string[];
}

export const usersApi = api.injectEndpoints({
  overrideExisting: false,
  endpoints: (builder) => ({
    getUserById: builder.query<User, { id: string }>({
      query: ({ id }) => ({
        url: `/users/${id}`,
      }),
      providesTags: ['Session', 'Users'],
    }),
    getListUsers: builder.query<PagedList<User>, GetListUsersRequest>({
      query: (params) => ({
        url: '/users',
        params,
      }),
      providesTags: ['Session', 'Users'],
    }),
    createUser: builder.mutation<void, CreateUserRequest>({
      query: (data) => ({
        url: '/users',
        method: 'POST',
        body: data,
      }),
      invalidatesTags: (_, error) => (error ? [] : ['Users']),
    }),
    updateUser: builder.mutation<void, UpdateUserRequest>({
      query: ({ userId, ...data }) => ({
        url: `/users/${userId}`,
        method: 'PUT',
        body: data,
      }),
      invalidatesTags: (_, error) => (error ? [] : ['Users']),
    }),
    deleteUser: builder.mutation<void, { id: string }>({
      query: ({ id }) => ({
        url: `/users/${id}`,
        method: 'DELETE',
      }),
      invalidatesTags: (_, error) => (error ? [] : ['Users']),
    }),
  }),
});

export const {
  useGetUserByIdQuery,
  useGetListUsersQuery,
  useCreateUserMutation,
  useUpdateUserMutation,
  useDeleteUserMutation,
} = usersApi;
