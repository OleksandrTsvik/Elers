import { api } from '.';
import { UserProfile } from '../models/user.interface';

interface UpdateCurrentProfileRequest {
  email: string;
  firstName: string;
  lastName: string;
  patronymic: string;
  birthDate?: Date;
}

interface ChangeCurrentUserPasswordRequest {
  currentPassword: string;
  newPassword: string;
  confirmPassword: string;
}

export const profileApi = api.injectEndpoints({
  overrideExisting: false,
  endpoints: (builder) => ({
    getCurrentProfile: builder.query<UserProfile, void>({
      query: () => ({
        url: '/profile',
      }),
      providesTags: ['Session', 'Users'],
    }),
    getMyEnrolledCourses: builder.query<{ id: string; title: string }[], void>({
      query: () => ({
        url: '/profile/enrolled-courses',
      }),
      providesTags: ['Session', 'CourseList', 'MyCourses'],
    }),
    updateCurrentProfile: builder.mutation<void, UpdateCurrentProfileRequest>({
      query: (data) => ({
        url: '/profile',
        method: 'PUT',
        body: data,
      }),
      invalidatesTags: (_, error) => (error ? [] : ['Session']),
    }),
    changeCurrentUserPassword: builder.mutation<
      void,
      ChangeCurrentUserPasswordRequest
    >({
      query: (data) => ({
        url: '/profile/password',
        method: 'PATCH',
        body: data,
      }),
    }),
    changeAvatar: builder.mutation<string, { avatar: Blob; filename?: string }>(
      {
        query: ({ avatar, filename }) => {
          const formData = new FormData();
          formData.append('avatar', avatar, filename);

          return {
            url: '/profile/avatar',
            method: 'PATCH',
            body: formData,
            responseHandler: 'text',
          };
        },
        invalidatesTags: (_, error) => (error ? [] : ['Session']),
      },
    ),
    deleteAvatar: builder.mutation<void, void>({
      query: () => ({
        url: '/profile/avatar',
        method: 'DELETE',
      }),
      invalidatesTags: (_, error) => (error ? [] : ['Session']),
    }),
  }),
});

export const {
  useGetCurrentProfileQuery,
  useGetMyEnrolledCoursesQuery,
  useUpdateCurrentProfileMutation,
  useChangeCurrentUserPasswordMutation,
  useChangeAvatarMutation,
  useDeleteAvatarMutation,
} = profileApi;
