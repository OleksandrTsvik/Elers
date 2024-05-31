import { createApi } from '@reduxjs/toolkit/query/react';

import { baseQueryWithReauth } from '../auth';
import {
  CourseMemberPermissions,
  CoursePermissionListItem,
} from '../models/course-permission.interface';

export const coursePermissionsApi = createApi({
  reducerPath: 'coursePermissionsApi',
  baseQuery: baseQueryWithReauth,
  endpoints: (builder) => ({
    getCourseMemberPermissions: builder.query<
      CourseMemberPermissions,
      { courseId?: string }
    >({
      query: ({ courseId }) => ({
        url: `/coursePermissions/${courseId}`,
      }),
    }),
    getListCoursePermissions: builder.query<CoursePermissionListItem[], void>({
      query: () => ({
        url: '/coursePermissions',
      }),
    }),
  }),
});

export const {
  useGetCourseMemberPermissionsQuery,
  useGetListCoursePermissionsQuery,
} = coursePermissionsApi;
