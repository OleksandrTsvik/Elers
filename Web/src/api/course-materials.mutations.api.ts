import { UploadFile } from 'antd';

import { api } from '.';
import { courseMaterialsQueriesApi } from './course-materials.queries.api';
import { ReorderItem } from '../common/types';
import { GradingMethod } from '../models/course-material.type';

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

interface CourseMaterialAssignmentRequest {
  title: string;
  description: string;
  deadline?: Date;
  maxFiles: number;
  maxGrade: number;
}

interface CreateCourseMaterialAssignmentRequest
  extends CourseMaterialAssignmentRequest {
  tabId: string;
}

interface UpdateCourseMaterialAssignmentRequest
  extends CourseMaterialAssignmentRequest {
  id?: string;
}

interface CourseMaterialTestRequest {
  title: string;
  description?: string;
  numberAttempts: number;
  timeLimitInMinutes?: number;
  deadline?: Date;
  gradingMethod: GradingMethod;
  shuffleQuestions: boolean;
}

interface CreateCourseMaterialTestRequest extends CourseMaterialTestRequest {
  tabId: string;
}

interface UpdateCourseMaterialTestRequest extends CourseMaterialTestRequest {
  id?: string;
}

interface ReorderCourseMaterialsRequest {
  reorders: ReorderItem[];
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
      invalidatesTags: (_, error) =>
        error ? [] : ['Course', 'CourseMaterialList'],
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
      invalidatesTags: (_, error) =>
        error ? [] : ['Course', 'CourseMaterialList'],
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
      invalidatesTags: (_, error) =>
        error ? [] : ['Course', 'CourseMaterialList'],
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
      invalidatesTags: (_, error) =>
        error ? [] : ['Course', 'CourseMaterialList'],
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
      invalidatesTags: (_, error) =>
        error ? [] : ['Course', 'CourseMaterialList'],
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
      invalidatesTags: (_, error) =>
        error ? [] : ['Course', 'CourseMaterialList'],
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
    createCourseMaterialAssignment: builder.mutation<
      void,
      CreateCourseMaterialAssignmentRequest
    >({
      query: ({ tabId, ...data }) => ({
        url: `/courseMaterials/assignment/${tabId}`,
        method: 'POST',
        body: data,
      }),
      invalidatesTags: (_, error) =>
        error ? [] : ['Course', 'CourseMaterialList'],
    }),
    updateCourseMaterialAssignment: builder.mutation<
      void,
      UpdateCourseMaterialAssignmentRequest
    >({
      query: ({ id, ...data }) => ({
        url: `/courseMaterials/assignment/${id}`,
        method: 'PUT',
        body: data,
      }),
      invalidatesTags: (_, error) =>
        error
          ? []
          : ['Course', 'CourseMaterialAssignment', 'CourseMaterialList'],
    }),
    createCourseMaterialTest: builder.mutation<
      string,
      CreateCourseMaterialTestRequest
    >({
      query: ({ tabId, ...data }) => ({
        url: `/courseMaterials/test/${tabId}`,
        method: 'POST',
        body: data,
      }),
      invalidatesTags: (_, error) =>
        error ? [] : ['Course', 'CourseMaterialList'],
    }),
    updateCourseMaterialTest: builder.mutation<
      void,
      UpdateCourseMaterialTestRequest
    >({
      query: ({ id, ...data }) => ({
        url: `/courseMaterials/test/${id}`,
        method: 'PUT',
        body: data,
      }),
      invalidatesTags: (_, error) =>
        error ? [] : ['Course', 'CourseMaterialTest', 'CourseMaterialList'],
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
      invalidatesTags: (_, error) =>
        error ? [] : ['Course', 'CourseList', 'CourseMaterialList'],
    }),
    reorderCourseMaterials: builder.mutation<
      void,
      ReorderCourseMaterialsRequest
    >({
      query: (data) => ({
        url: '/courseMaterials/reorder',
        method: 'PUT',
        body: data,
      }),
      invalidatesTags: (_, error) =>
        error ? [] : ['Course', 'CourseMaterialList'],
    }),
    moveMaterialToAnotherTab: builder.mutation<
      void,
      { materialId: string; newCourseTabId: string }
    >({
      query: ({ materialId, ...data }) => ({
        url: `/courseMaterials/move/${materialId}`,
        method: 'PATCH',
        body: data,
      }),
      invalidatesTags: (_, error) =>
        error ? [] : ['Course', 'CourseMaterialList'],
    }),
    deleteCourseMaterial: builder.mutation<void, { id: string }>({
      query: ({ id }) => ({
        url: `/courseMaterials/${id}`,
        method: 'DELETE',
      }),
      invalidatesTags: (_, error) =>
        error ? [] : ['Course', 'CourseMaterialList'],
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
  useCreateCourseMaterialAssignmentMutation,
  useUpdateCourseMaterialAssignmentMutation,
  useCreateCourseMaterialTestMutation,
  useUpdateCourseMaterialTestMutation,
  useUpdateCourseMaterialActiveMutation,
  useReorderCourseMaterialsMutation,
  useMoveMaterialToAnotherTabMutation,
  useDeleteCourseMaterialMutation,
} = courseMaterialsMutationsApi;
