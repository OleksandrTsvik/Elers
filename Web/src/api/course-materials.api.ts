import { UploadFile } from 'antd';

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

interface GetCourseMaterialLinkResponse extends GetCourseMaterialResponse {
  title: string;
  link: string;
}

interface CreateCourseMaterialContentRequest {
  tabId: string;
  content: string;
}

interface UpdateCourseMaterialContentRequest {
  id: string;
  content: string;
}

interface CreateCourseMaterialLinkRequest {
  tabId: string;
  title: string;
  link: string;
}

interface UpdateCourseMaterialLinkRequest {
  id: string;
  title: string;
  link: string;
}

interface CreateCourseMaterialFileRequest {
  tabId: string;
  title: string;
  file: UploadFile;
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
    getCourseMaterialLink: builder.query<
      GetCourseMaterialLinkResponse,
      { tabId?: string; id?: string }
    >({
      query: ({ tabId, id }) => ({
        url: `/courseMaterials/${tabId}/link/${id}`,
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
    getListCourseMaterialsByTabIdToEdit: builder.query<
      CourseMaterial[],
      { id: string }
    >({
      query: ({ id }) => ({
        url: `/courseMaterials/tabs/edit/${id}`,
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
    createCourseMaterialLink: builder.mutation<
      string,
      CreateCourseMaterialLinkRequest
    >({
      query: ({ tabId, ...data }) => ({
        url: `/courseMaterials/link/${tabId}`,
        method: 'POST',
        body: data,
      }),
      invalidatesTags: ['Course', 'CourseMaterialList'],
    }),
    updateCourseMaterialLink: builder.mutation<
      string,
      UpdateCourseMaterialLinkRequest
    >({
      query: ({ id, ...data }) => ({
        url: `/courseMaterials/link/${id}`,
        method: 'PUT',
        body: data,
      }),
      invalidatesTags: ['Course', 'CourseMaterialList'],
    }),
    createCourseMaterialFile: builder.mutation<
      string,
      CreateCourseMaterialFileRequest
    >({
      query: ({ tabId, ...data }) => {
        const formData = new FormData();
        formData.append('title', data.title);
        formData.append('file', data.file.originFileObj as Blob);

        return {
          url: `/courseMaterials/file/${tabId}`,
          method: 'POST',
          body: formData,
        };
      },
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
  useGetCourseMaterialLinkQuery,
  useGetListCourseMaterialsByTabIdQuery,
  useGetListCourseMaterialsByTabIdToEditQuery,
  useCreateCourseMaterialContentMutation,
  useUpdateCourseMaterialContentMutation,
  useCreateCourseMaterialLinkMutation,
  useUpdateCourseMaterialLinkMutation,
  useCreateCourseMaterialFileMutation,
  useUpdateCourseMaterialActiveMutation,
  useDeleteCourseMaterialMutation,
} = courseMaterialsApi;
