import { coursesApi } from './courses.api';

interface CreateCourseTabRequest {
  courseId: string;
  name: string;
}

interface UpdateCourseTabRequest {
  tabId: string;
  name?: string;
  isActive?: boolean;
  showMaterialsCount?: boolean;
}

interface UpdateCourseTabColorRequest {
  tabId: string;
  color?: string;
}

export const courseTabsApi = coursesApi.injectEndpoints({
  endpoints: (builder) => ({
    createCourseTab: builder.mutation<string, CreateCourseTabRequest>({
      query: ({ courseId, ...data }) => ({
        url: `/courseTabs/${courseId}`,
        method: 'POST',
        body: data,
      }),
      invalidatesTags: ['Course'],
    }),
    updateCourseTab: builder.mutation<void, UpdateCourseTabRequest>({
      query: ({ tabId, ...data }) => ({
        url: `/courseTabs/${tabId}`,
        method: 'PUT',
        body: data,
      }),
      invalidatesTags: ['Course'],
    }),
    updateCourseTabColor: builder.mutation<void, UpdateCourseTabColorRequest>({
      query: ({ tabId, ...data }) => ({
        url: `/courseTabs/color/${tabId}`,
        method: 'PATCH',
        body: data,
      }),
      invalidatesTags: ['Course'],
    }),
    deleteCourseTab: builder.mutation<void, { id: string }>({
      query: ({ id }) => ({
        url: `/courseTabs/${id}`,
        method: 'DELETE',
      }),
      invalidatesTags: ['Course'],
    }),
  }),
});

export const {
  useCreateCourseTabMutation,
  useUpdateCourseTabMutation,
  useUpdateCourseTabColorMutation,
  useDeleteCourseTabMutation,
} = courseTabsApi;
