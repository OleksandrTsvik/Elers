import { UploadFile } from 'antd';

import { api } from '.';
import { courseMaterialsQueriesApi } from './course-materials.queries.api';

interface CreateCourseMaterialContentRequest {
  tabId: string;
  content: string;
}

interface UpdateCourseMaterialContentRequest {
  tabId: string;
  id: string;
  content: string;
}

interface CreateCourseMaterialLinkRequest {
  tabId: string;
  title: string;
  link: string;
}

interface UpdateCourseMaterialLinkRequest {
  tabId: string;
  id: string;
  title: string;
  link: string;
}

interface CreateCourseMaterialFileRequest {
  tabId: string;
  title: string;
  file: UploadFile;
}

interface UpdateCourseMaterialFileRequest {
  tabId: string;
  id: string;
  title: string;
  file?: UploadFile;
}

export const courseMaterialsMutationsApi = api.injectEndpoints({
  overrideExisting: false,
  endpoints: (builder) => ({
    createCourseMaterialContent: builder.mutation<
      void,
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
      void,
      UpdateCourseMaterialContentRequest
    >({
      query: ({ id, content }) => ({
        url: `/courseMaterials/content/${id}`,
        method: 'PUT',
        body: { content },
      }),
      invalidatesTags: ['Course', 'CourseMaterialList'],
      async onQueryStarted(
        { tabId, id, ...patch },
        { dispatch, queryFulfilled },
      ) {
        try {
          await queryFulfilled;

          dispatch(
            courseMaterialsQueriesApi.util.updateQueryData(
              'getCourseMaterialContent',
              { tabId, id },
              (draft) => {
                Object.assign(draft, patch);
              },
            ),
          );
        } catch {
          /* empty */
        }
      },
    }),
    createCourseMaterialLink: builder.mutation<
      void,
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
      void,
      UpdateCourseMaterialLinkRequest
    >({
      query: ({ id, title, link }) => ({
        url: `/courseMaterials/link/${id}`,
        method: 'PUT',
        body: { title, link },
      }),
      invalidatesTags: ['Course', 'CourseMaterialList'],
      async onQueryStarted(
        { tabId, id, ...patch },
        { dispatch, queryFulfilled },
      ) {
        try {
          await queryFulfilled;

          dispatch(
            courseMaterialsQueriesApi.util.updateQueryData(
              'getCourseMaterialLink',
              { tabId, id },
              (draft) => {
                Object.assign(draft, patch);
              },
            ),
          );
        } catch {
          /* empty */
        }
      },
    }),
    createCourseMaterialFile: builder.mutation<
      void,
      CreateCourseMaterialFileRequest
    >({
      query: ({ tabId, title, file }) => {
        const formData = new FormData();
        formData.append('title', title);
        formData.append('file', file.originFileObj as Blob);

        return {
          url: `/courseMaterials/file/${tabId}`,
          method: 'POST',
          body: formData,
        };
      },
      invalidatesTags: ['Course', 'CourseMaterialList'],
    }),
    updateCourseMaterialFile: builder.mutation<
      void,
      UpdateCourseMaterialFileRequest
    >({
      query: ({ id, title, file }) => {
        const formData = new FormData();
        formData.append('title', title);

        if (file) {
          formData.append('file', file.originFileObj as Blob);
        }

        return {
          url: `/courseMaterials/file/${id}`,
          method: 'PUT',
          body: formData,
        };
      },
      invalidatesTags: ['Course', 'CourseMaterialList'],
      async onQueryStarted(
        { tabId, id, ...patch },
        { dispatch, queryFulfilled },
      ) {
        try {
          await queryFulfilled;

          dispatch(
            courseMaterialsQueriesApi.util.updateQueryData(
              'getCourseMaterialFile',
              { tabId, id },
              (draft) => {
                Object.assign(draft, { fileTitle: patch.title });
              },
            ),
          );
        } catch {
          /* empty */
        }
      },
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
  useCreateCourseMaterialContentMutation,
  useUpdateCourseMaterialContentMutation,
  useCreateCourseMaterialLinkMutation,
  useUpdateCourseMaterialLinkMutation,
  useCreateCourseMaterialFileMutation,
  useUpdateCourseMaterialFileMutation,
  useUpdateCourseMaterialActiveMutation,
  useDeleteCourseMaterialMutation,
} = courseMaterialsMutationsApi;