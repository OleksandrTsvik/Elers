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
      invalidatesTags: ['CourseMemberList'],
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
      invalidatesTags: ['CourseMemberList'],
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
  }),
});

export const { useEnrollToCourseMutation, useUnenrollFromCourseMutation } =
  courseMembersApi;
