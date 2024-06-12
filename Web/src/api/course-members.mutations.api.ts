import { api } from '.';
import { coursePermissionsApi } from './course-permissions.api';

export const courseMembersApi = api.injectEndpoints({
  overrideExisting: false,
  endpoints: (builder) => ({
    enrollToCourse: builder.mutation<void, { courseId: string }>({
      query: ({ courseId }) => ({
        url: `/courseMembers/enroll/${courseId}`,
        method: 'POST',
      }),
      invalidatesTags: (_, error) =>
        error ? [] : ['MyCourses', 'CourseMemberList'],
      async onQueryStarted({ courseId }, { dispatch, queryFulfilled }) {
        try {
          await queryFulfilled;

          dispatch(
            coursePermissionsApi.util.updateQueryData(
              'getCourseMemberPermissions',
              { courseId },
              (draft) => {
                Object.assign(draft, { isMember: true });
              },
            ),
          );
        } catch {
          /* empty */
        }
      },
    }),
    unenrollFromCourse: builder.mutation<void, { courseId: string }>({
      query: ({ courseId }) => ({
        url: `/courseMembers/unenroll/${courseId}`,
        method: 'POST',
      }),
      invalidatesTags: (_, error) =>
        error ? [] : ['MyCourses', 'CourseMemberList'],
      async onQueryStarted({ courseId }, { dispatch, queryFulfilled }) {
        try {
          await queryFulfilled;

          dispatch(
            coursePermissionsApi.util.updateQueryData(
              'getCourseMemberPermissions',
              { courseId },
              (draft) => {
                Object.assign(draft, { isMember: false });
              },
            ),
          );
        } catch {
          /* empty */
        }
      },
    }),
    changeCourseMemberRole: builder.mutation<
      void,
      { memberId: string; courseRoleId?: string }
    >({
      query: ({ memberId, ...data }) => ({
        url: `/courseMembers/roles/${memberId}`,
        method: 'PUT',
        body: data,
      }),
      invalidatesTags: (_, error) => (error ? [] : ['CourseMemberList']),
    }),
    removeCourseMember: builder.mutation<void, { memberId: string }>({
      query: ({ memberId }) => ({
        url: `/courseMembers/${memberId}`,
        method: 'DELETE',
      }),
      invalidatesTags: (_, error) => (error ? [] : ['CourseMemberList']),
    }),
  }),
});

export const {
  useEnrollToCourseMutation,
  useUnenrollFromCourseMutation,
  useChangeCourseMemberRoleMutation,
  useRemoveCourseMemberMutation,
} = courseMembersApi;
