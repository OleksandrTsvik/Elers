import { api } from '.';
import { CourseMaterial, GradingMethod } from '../models/course-material.type';
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

interface CourseMaterialResponse {
  id: string;
  courseTabId: string;
  isActive: boolean;
  order: number;
}

interface GetCourseMaterialAssignmentResponse extends CourseMaterialResponse {
  title: string;
  description: string;
  deadline?: Date;
  maxFiles: number;
  maxGrade: number;
}

interface GetCourseMaterialTestResponse extends CourseMaterialResponse {
  title: string;
  description?: string;
  numberAttempts: number;
  timeLimitInMinutes?: number;
  deadline?: Date;
  gradingMethod: GradingMethod;
  shuffleQuestions: boolean;
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
    getCourseMaterialTest: builder.query<
      GetCourseMaterialTestResponse,
      { id?: string }
    >({
      query: ({ id }) => ({
        url: `/courseMaterials/test/${id}`,
      }),
      providesTags: ['Session', 'CourseMaterialTest'],
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
  useGetCourseMaterialTestQuery,
  useGetListCourseMaterialsByTabIdQuery,
  useGetListCourseMaterialsByTabIdToEditQuery,
} = courseMaterialsQueriesApi;
