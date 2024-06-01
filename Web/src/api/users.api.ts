import { api } from '.';
import { User } from '../models/user.interface';

interface CreateUserRequest {
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  patronymic: string;
  roleIds: string[];
}

interface UpdateUserRequest {
  userId: string;
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
    getListUsers: builder.query<User[], void>({
      query: () => ({
        url: '/users',
      }),
      providesTags: ['Session', 'Users'],
    }),
    createUser: builder.mutation<void, CreateUserRequest>({
      query: (data) => ({
        url: '/users',
        method: 'POST',
        body: data,
      }),
      invalidatesTags: ['Users'],
    }),
    updateUser: builder.mutation<void, UpdateUserRequest>({
      query: ({ userId, ...data }) => ({
        url: `/users/${userId}`,
        method: 'PUT',
        body: data,
      }),
      invalidatesTags: ['Users'],
    }),
    deleteUser: builder.mutation<void, { id: string }>({
      query: ({ id }) => ({
        url: `/users/${id}`,
        method: 'DELETE',
      }),
      invalidatesTags: ['Users'],
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
