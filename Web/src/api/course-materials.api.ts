import { coursesApi } from './courses.api';
import { CourseMaterial } from '../models/course-material.type';

interface CreateCourseMaterialContentRequest {
  tabId: string;
  content: string;
}

export const courseMaterialsApi = coursesApi.injectEndpoints({
  overrideExisting: false,
  endpoints: (builder) => ({
    getListCourseMaterialsByTabId: builder.query<
      CourseMaterial[],
      { id: string }
    >({
      query: ({ id }) => ({
        url: `/courseMaterials/tabs/${id}`,
      }),
      providesTags: ['CourseMaterialList'],
    }),
    createCourseMaterialContent: builder.mutation<
      string,
      CreateCourseMaterialContentRequest
    >({
      query: ({ tabId, ...data }) => ({
        url: `/courseMaterials/content/${tabId}`,
        method: 'POST',
        body: data,
      }),
      invalidatesTags: ['Course', 'CourseMaterialList'],
    }),
    updateCourseMaterialActive: builder.mutation<
      void,
      { id: string; isActive: boolean }
    >({
      query: ({ id, ...data }) => ({
        url: `/courseMaterials/active/${id}`,
        method: 'PATCH',
        body: data,
      }),
      invalidatesTags: ['Course', 'CourseMaterialList'],
    }),
    deleteCourseMaterial: builder.mutation<void, { id: string }>({
      query: ({ id }) => ({
        url: `/courseMaterials/${id}`,
        method: 'DELETE',
      }),
      invalidatesTags: ['Course', 'CourseMaterialList'],
    }),
  }),
});

export const {
  useGetListCourseMaterialsByTabIdQuery,
  useCreateCourseMaterialContentMutation,
  useUpdateCourseMaterialActiveMutation,
  useDeleteCourseMaterialMutation,
} = courseMaterialsApi;
