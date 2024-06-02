import { api } from '.';
import { CourseRoleListItem } from '../models/course-role.interface';

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

export const courseRolesApi = api.injectEndpoints({
  overrideExisting: false,
  endpoints: (builder) => ({
    getListCourseRoles: builder.query<
      CourseRoleListItem[],
      { courseId?: string }
    >({
      query: ({ courseId }) => ({
        url: `/courseRoles/${courseId}`,
      }),
      providesTags: ['Session', 'Locale', 'CourseRoles'],
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
      invalidatesTags: (_, error) =>
        error ? [] : ['CourseRoles', 'CourseMemberList'],
    }),
    deleteCourseRole: builder.mutation<void, { roleId: string }>({
      query: ({ roleId }) => ({
        url: `/courseRoles/${roleId}`,
        method: 'DELETE',
      }),
      invalidatesTags: (_, error) =>
        error ? [] : ['CourseRoles', 'CourseMemberList'],
    }),
  }),
});

export const {
  useGetListCourseRolesQuery,
  useCreateCourseRoleMutation,
  useUpdateCourseRoleMutation,
  useDeleteCourseRoleMutation,
} = courseRolesApi;
