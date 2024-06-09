import { api } from '.';
import { TestQuestionType } from '../models/test-question.interface';

interface TestQuestionResponse {
  id: string;
  testId: string;
  text: string;
  points: number;
  createdAt: Date;
}

interface GetTestQuestionInputResponse extends TestQuestionResponse {
  answer: string;
}

interface GetTestQuestionIdsAndTypes {
  id: string;
  type: TestQuestionType;
}

interface CreateTestQuestion {
  testId: string;
  text: string;
  points: number;
}

interface CreateTestQuestionInputRequest extends CreateTestQuestion {
  answer: string;
}

interface UpdateTestQuestion {
  id?: string;
  text: string;
  points: number;
}

interface UpdateTestQuestionInputRequest extends UpdateTestQuestion {
  answer: string;
}

export const testQuestionsApi = api.injectEndpoints({
  overrideExisting: false,
  endpoints: (builder) => ({
    getTestQuestionInput: builder.query<
      GetTestQuestionInputResponse,
      { id?: string }
    >({
      query: ({ id }) => ({
        url: `/testQuestions/${id}`,
      }),
      providesTags: ['Session', 'TestQuestion'],
    }),
    getTestQuestionIdsAndTypes: builder.query<
      GetTestQuestionIdsAndTypes[],
      { testId?: string }
    >({
      query: ({ testId }) => ({
        url: `/testQuestions/ids-and-types/${testId}`,
      }),
      providesTags: ['Session', 'TestQuestionIdsAndTypes'],
    }),
    createTestQuestionInput: builder.mutation<
      void,
      CreateTestQuestionInputRequest
    >({
      query: ({ testId, ...data }) => ({
        url: `/testQuestions/input/${testId}`,
        method: 'POST',
        body: data,
      }),
      invalidatesTags: (_, error) =>
        error ? [] : ['TestQuestion', 'TestQuestionIdsAndTypes'],
    }),
    updateTestQuestionInput: builder.mutation<
      void,
      UpdateTestQuestionInputRequest
    >({
      query: ({ id, ...data }) => ({
        url: `/testQuestions/input/${id}`,
        method: 'PUT',
        body: data,
      }),
      invalidatesTags: (_, error) => (error ? [] : ['TestQuestion']),
    }),
  }),
});

export const {
  useGetTestQuestionInputQuery,
  useGetTestQuestionIdsAndTypesQuery,
  useCreateTestQuestionInputMutation,
  useUpdateTestQuestionInputMutation,
} = testQuestionsApi;
