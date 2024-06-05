import { api } from '.';
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

interface GetCourseMaterialFileResponse extends GetCourseMaterialResponse {
  fileTitle: string;
}

interface GetCourseMaterialAssignmentResponse {
  id: string;
  courseTabId: string;
  isActive: boolean;
  order: number;

  title: string;
  description: string;
  deadline?: Date;
  maxFiles: number;
  maxGrade: number;
}

export const courseMaterialsQueriesApi = api.injectEndpoints({
  overrideExisting: false,
  endpoints: (builder) => ({
    getCourseMaterialContent: builder.query<
      GetCourseMaterialContentResponse,
      { tabId?: string; id?: string }
    >({
      query: ({ tabId, id }) => ({
        url: `/courseMaterials/${tabId}/content/${id}`,
      }),
      providesTags: ['Session'],
    }),
    getCourseMaterialLink: builder.query<
      GetCourseMaterialLinkResponse,
      { tabId?: string; id?: string }
    >({
      query: ({ tabId, id }) => ({
        url: `/courseMaterials/${tabId}/link/${id}`,
      }),
      providesTags: ['Session'],
    }),
    getCourseMaterialFile: builder.query<
      GetCourseMaterialFileResponse,
      { tabId?: string; id?: string }
    >({
      query: ({ tabId, id }) => ({
        url: `/courseMaterials/${tabId}/file/${id}`,
      }),
      providesTags: ['Session'],
    }),
    getCourseMaterialAssignment: builder.query<
      GetCourseMaterialAssignmentResponse,
      { id?: string }
    >({
      query: ({ id }) => ({
        url: `/courseMaterials/assignment/${id}`,
      }),
      providesTags: ['Session', 'CourseMaterialAssignment'],
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
      providesTags: ['Session', 'CourseMaterialList'],
    }),
  }),
});

export const {
  useGetCourseMaterialContentQuery,
  useGetCourseMaterialLinkQuery,
  useGetCourseMaterialFileQuery,
  useGetCourseMaterialAssignmentQuery,
  useGetListCourseMaterialsByTabIdQuery,
  useGetListCourseMaterialsByTabIdToEditQuery,
} = courseMaterialsQueriesApi;
