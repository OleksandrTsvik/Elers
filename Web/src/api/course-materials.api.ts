import { coursesApi } from './courses.api';
import { CourseMaterial } from '../models/course-material.type';
import { CourseTabType } from '../shared';

interface GetCourseMaterialResponse {
  id: string;
  courseId: string;
  tabId: string;
  courseTitle: string;
  courseTabType: CourseTabType;
}

interface GetCourseMaterialContentResponse extends GetCourseMaterialResponse {
  content: string;
}

interface CreateCourseMaterialContentRequest {
  tabId: string;
  content: string;
}

interface UpdateCourseMaterialContentRequest {
  id: string;
  content: string;
}

export const courseMaterialsApi = coursesApi.injectEndpoints({
  overrideExisting: false,
  endpoints: (builder) => ({
    getCourseMaterialContent: builder.query<
      GetCourseMaterialContentResponse,
      { tabId?: string; id?: string }
    >({
      query: ({ tabId, id }) => ({
        url: `/courseMaterials/${tabId}/content/${id}`,
      }),
      providesTags: ['CourseMaterialList'],
    }),
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
    updateCourseMaterialContent: builder.mutation<
      string,
      UpdateCourseMaterialContentRequest
    >({
      query: ({ id, ...data }) => ({
        url: `/courseMaterials/content/${id}`,
        method: 'PUT',
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
  useGetCourseMaterialContentQuery,
  useGetListCourseMaterialsByTabIdQuery,
  useCreateCourseMaterialContentMutation,
  useUpdateCourseMaterialContentMutation,
  useUpdateCourseMaterialActiveMutation,
  useDeleteCourseMaterialMutation,
} = courseMaterialsApi;
