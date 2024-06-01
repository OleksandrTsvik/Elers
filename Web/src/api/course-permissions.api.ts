import { api } from '.';
import {
  CourseMemberPermissions,
  CoursePermissionListItem,
} from '../models/course-permission.interface';

export const coursePermissionsApi = api.injectEndpoints({
  overrideExisting: false,
  endpoints: (builder) => ({
    getCourseMemberPermissions: builder.query<
      CourseMemberPermissions,
      { courseId?: string }
    >({
      query: ({ courseId }) => ({
        url: `/coursePermissions/${courseId}`,
      }),
      providesTags: ['Session'],
    }),
    getListCoursePermissions: builder.query<CoursePermissionListItem[], void>({
      query: () => ({
        url: '/coursePermissions',
      }),
      providesTags: ['Session', 'Locale'],
    }),
  }),
});

export const {
  useGetCourseMemberPermissionsQuery,
  useGetListCoursePermissionsQuery,
} = coursePermissionsApi;
