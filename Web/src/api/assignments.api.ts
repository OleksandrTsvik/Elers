import { UploadFile } from 'antd';

import { api } from '.';
import { PagedList, PagingParams } from '../common/types';
import {
  Assignment,
  SubmittedAssignmentReview,
  SubmittedAssignmentStatus,
} from '../models/assignment.interface';

export interface SubmittedAssignmentListItemResponse {
  submittedAssignmentId: string;
  assignmentId: string;
  studentId: string;
  assignmentTitle: string;
  submittedDate: Date;
  studentFirstName?: string;
  studentLastName?: string;
  studentPatronymic?: string;
}

interface GetListSubmittedAssignmentsRequest extends PagingParams {
  courseId?: string;
  status?: SubmittedAssignmentStatus;
  assignmentId?: string;
  studentId?: string;
}

interface AssignmentTitleItemResponse {
  assignmentId: string;
  title: string;
}

interface SubmitAssignmentRequest {
  id: string;
  text?: string;
  files?: UploadFile[];
}

interface GradeAssignmentRequest {
  submittedAssignmentId: string;
  status: SubmittedAssignmentStatus;
  grade: number;
  comment?: string;
}

export const assignmentsApi = api.injectEndpoints({
  overrideExisting: false,
  endpoints: (builder) => ({
    getAssignment: builder.query<Assignment, { id?: string }>({
      query: ({ id }) => ({
        url: `/assignments/${id}`,
      }),
      providesTags: ['Session', 'Assignment', 'CourseMaterialAssignment'],
    }),
    getListSubmittedAssignments: builder.query<
      PagedList<SubmittedAssignmentListItemResponse>,
      GetListSubmittedAssignmentsRequest
    >({
      query: ({ courseId, ...params }) => ({
        url: `/assignments/submitted/${courseId}`,
        params,
      }),
      providesTags: [
        'Session',
        'Assignment',
        'CourseMaterialAssignment',
        'CourseMemberList',
      ],
    }),
    getListAssignmentTitles: builder.query<
      AssignmentTitleItemResponse[],
      { courseId?: string }
    >({
      query: ({ courseId }) => ({
        url: `/assignments/titles/${courseId}`,
      }),
      providesTags: ['CourseMaterialAssignment', 'CourseMaterialList'],
    }),
    getSubmittedAssignment: builder.query<
      SubmittedAssignmentReview,
      { submittedAssignmentId?: string }
    >({
      query: ({ submittedAssignmentId }) => ({
        url: `/assignments/review/${submittedAssignmentId}`,
      }),
      providesTags: [
        'Session',
        'Assignment',
        'CourseMaterialAssignment',
        'CourseMaterialList',
      ],
    }),
    submitAssignment: builder.mutation<void, SubmitAssignmentRequest>({
      query: ({ id, text, files }) => {
        const formData = new FormData();

        if (text) {
          formData.append('text', text);
        }

        if (files) {
          files.forEach((file) =>
            formData.append('files', file.originFileObj as Blob),
          );
        }

        return {
          url: `/assignments/${id}`,
          method: 'POST',
          body: formData,
        };
      },
      invalidatesTags: (_, error) => (error ? [] : ['Assignment']),
    }),
    gradeAssignment: builder.mutation<void, GradeAssignmentRequest>({
      query: ({ submittedAssignmentId, ...data }) => ({
        url: `/assignments/grade/${submittedAssignmentId}`,
        method: 'POST',
        body: data,
      }),
      invalidatesTags: (_, error) => (error ? [] : ['Assignment']),
    }),
  }),
});

export const {
  useGetAssignmentQuery,
  useGetListSubmittedAssignmentsQuery,
  useGetListAssignmentTitlesQuery,
  useGetSubmittedAssignmentQuery,
  useSubmitAssignmentMutation,
  useGradeAssignmentMutation,
} = assignmentsApi;
