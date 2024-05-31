import { createApi } from '@reduxjs/toolkit/query/react';

import { baseQueryWithReauth } from '../auth';
import { CourseRoleListItem } from '../models/course-role.interface';

interface GetListCourseRolesResponse {
  courseId: string;
  courseTitle: string;
  courseRoles: CourseRoleListItem[];
}

interface CreateCourseRoleRequest {
  courseId: string;
  name: string;
  permissionIds: string[];
}

interface UpdateCourseRoleRequest {
  roleId: string;
  name: string;
  permissionIds: string[];
}

export const courseRolesApi = createApi({
  reducerPath: 'courseRolesApi',
  baseQuery: baseQueryWithReauth,
  tagTypes: ['CourseRoles'],
  endpoints: (builder) => ({
    getListCourseRoles: builder.query<
      GetListCourseRolesResponse,
      { courseId?: string }
    >({
      query: ({ courseId }) => ({
        url: `/courseRoles/${courseId}`,
      }),
      providesTags: ['CourseRoles'],
    }),
    createCourseRole: builder.mutation<void, CreateCourseRoleRequest>({
      query: ({ courseId, ...data }) => ({
        url: `/courseRoles/${courseId}`,
        method: 'POST',
        body: data,
      }),
      invalidatesTags: (_, error) => (error ? [] : ['CourseRoles']),
    }),
    updateCourseRole: builder.mutation<void, UpdateCourseRoleRequest>({
      query: ({ roleId, ...data }) => ({
        url: `/courseRoles/${roleId}`,
        method: 'PUT',
        body: data,
      }),
      invalidatesTags: (_, error) => (error ? [] : ['CourseRoles']),
    }),
    deleteCourseRole: builder.mutation<void, { roleId: string }>({
      query: ({ roleId }) => ({
        url: `/courseRoles/${roleId}`,
        method: 'DELETE',
      }),
      invalidatesTags: (_, error) => (error ? [] : ['CourseRoles']),
    }),
  }),
});

export const {
  useGetListCourseRolesQuery,
  useCreateCourseRoleMutation,
  useUpdateCourseRoleMutation,
  useDeleteCourseRoleMutation,
} = courseRolesApi;
