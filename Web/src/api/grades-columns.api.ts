import { api } from '.';

interface ManualGradesColumnRequest {
  title: string;
  date: Date;
  maxGrade: number;
}

interface CreateManualGradesColumnRequest extends ManualGradesColumnRequest {
  courseId: string | undefined;
}

interface UpdateManualGradesColumnRequest extends ManualGradesColumnRequest {
  columnId: string;
}

export const gradesColumnsApi = api.injectEndpoints({
  overrideExisting: false,
  endpoints: (builder) => ({
    createManualGradesColumn: builder.mutation<
      void,
      CreateManualGradesColumnRequest
    >({
      query: ({ courseId, ...data }) => ({
        url: `/manualGradesColumns/${courseId}`,
        method: 'POST',
        body: data,
      }),
      invalidatesTags: (_, error) => (error ? [] : ['Grades']),
    }),
    updateManualGradesColumn: builder.mutation<
      void,
      UpdateManualGradesColumnRequest
    >({
      query: ({ columnId, ...data }) => ({
        url: `/manualGradesColumns/${columnId}`,
        method: 'PUT',
        body: data,
      }),
      invalidatesTags: (_, error) => (error ? [] : ['Grades']),
    }),
    deleteManualGradesColumn: builder.mutation<void, { columnId: string }>({
      query: ({ columnId }) => ({
        url: `/manualGradesColumns/${columnId}`,
        method: 'DELETE',
      }),
      invalidatesTags: (_, error) => (error ? [] : ['Grades']),
    }),
  }),
});

export const {
  useCreateManualGradesColumnMutation,
  useUpdateManualGradesColumnMutation,
  useDeleteManualGradesColumnMutation,
} = gradesColumnsApi;
