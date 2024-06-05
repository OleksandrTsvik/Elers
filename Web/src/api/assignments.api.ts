import { UploadFile } from 'antd';

import { api } from '.';
import { Assignment } from '../models/assignment.interface';

interface SubmitAssignmentRequest {
  id: string;
  text?: string;
  files?: UploadFile[];
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
  }),
});

export const { useGetAssignmentQuery, useSubmitAssignmentMutation } =
  assignmentsApi;
