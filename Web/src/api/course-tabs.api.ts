import { coursesApi } from './courses.api';

interface CreateCourseTabRequest {
  courseId: string;
  name: string;
}

interface UpdateCourseTabNameRequest {
  tabId: string;
  name: string;
}

interface UpdateCourseTabColorRequest {
  tabId: string;
  color?: string;
}

export const courseTabsApi = coursesApi.injectEndpoints({
  endpoints: (builder) => ({
    createCourseTab: builder.mutation<void, CreateCourseTabRequest>({
      query: ({ courseId, ...data }) => ({
        url: `/courseTabs/${courseId}`,
        method: 'POST',
        body: data,
      }),
      invalidatesTags: ['Course'],
    }),
    updateCourseTabName: builder.mutation<void, UpdateCourseTabNameRequest>({
      query: ({ tabId, ...data }) => ({
        url: `/courseTabs/name/${tabId}`,
        method: 'PATCH',
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
  useUpdateCourseTabNameMutation,
  useUpdateCourseTabColorMutation,
  useDeleteCourseTabMutation,
} = courseTabsApi;
