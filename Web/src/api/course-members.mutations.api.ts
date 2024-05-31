import { createApi } from '@reduxjs/toolkit/query/react';

import { coursePermissionsApi } from './course-permissions.api';
import { baseQueryWithReauth } from '../auth';

export const courseMembersApi = createApi({
  reducerPath: 'courseMembersApi',
  baseQuery: baseQueryWithReauth,
  endpoints: (builder) => ({
    enrollToCourse: builder.mutation<void, { courseId: string }>({
      query: ({ courseId }) => ({
        url: `/courseMembers/enroll/${courseId}`,
        method: 'POST',
      }),
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
