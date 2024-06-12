import { api } from '.';
import {
  TestQuestion,
  TestQuestionChoiceOption,
  TestQuestionMatchOption,
  TestQuestionType,
} from '../models/test-question.interface';

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

interface CreateTestQuestionSingleChoiceRequest extends CreateTestQuestion {
  options: TestQuestionChoiceOption[];
}

interface CreateTestQuestionMultipleChoiceRequest extends CreateTestQuestion {
  options: TestQuestionChoiceOption[];
}

interface CreateTestQuestionMatchingRequest extends CreateTestQuestion {
  options: TestQuestionMatchOption[];
}

interface UpdateTestQuestion {
  id?: string;
  text: string;
  points: number;
}

interface UpdateTestQuestionInputRequest extends UpdateTestQuestion {
  answer: string;
}

interface UpdateTestQuestionSingleChoiceRequest extends UpdateTestQuestion {
  options: TestQuestionChoiceOption[];
}

interface UpdateTestQuestionMultipleChoiceRequest extends UpdateTestQuestion {
  options: TestQuestionChoiceOption[];
}

interface UpdateTestQuestionMatchingRequest extends UpdateTestQuestion {
  options: TestQuestionMatchOption[];
}

export const testQuestionsApi = api.injectEndpoints({
  overrideExisting: false,
  endpoints: (builder) => ({
    getTestQuestion: builder.query<TestQuestion, { id?: string }>({
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
    createTestQuestionSingleChoice: builder.mutation<
      void,
      CreateTestQuestionSingleChoiceRequest
    >({
      query: ({ testId, ...data }) => ({
        url: `/testQuestions/single-choice/${testId}`,
        method: 'POST',
        body: data,
      }),
      invalidatesTags: (_, error) =>
        error ? [] : ['TestQuestion', 'TestQuestionIdsAndTypes'],
    }),
    updateTestQuestionSingleChoice: builder.mutation<
      void,
      UpdateTestQuestionSingleChoiceRequest
    >({
      query: ({ id, ...data }) => ({
        url: `/testQuestions/single-choice/${id}`,
        method: 'PUT',
        body: data,
      }),
      invalidatesTags: (_, error) => (error ? [] : ['TestQuestion']),
    }),
    createTestQuestionMultipleChoice: builder.mutation<
      void,
      CreateTestQuestionMultipleChoiceRequest
    >({
      query: ({ testId, ...data }) => ({
        url: `/testQuestions/multiple-choice/${testId}`,
        method: 'POST',
        body: data,
      }),
      invalidatesTags: (_, error) =>
        error ? [] : ['TestQuestion', 'TestQuestionIdsAndTypes'],
    }),
    updateTestQuestionMultipleChoice: builder.mutation<
      void,
      UpdateTestQuestionMultipleChoiceRequest
    >({
      query: ({ id, ...data }) => ({
        url: `/testQuestions/multiple-choice/${id}`,
        method: 'PUT',
        body: data,
      }),
      invalidatesTags: (_, error) => (error ? [] : ['TestQuestion']),
    }),
    createTestQuestionMatching: builder.mutation<
      void,
      CreateTestQuestionMatchingRequest
    >({
      query: ({ testId, ...data }) => ({
        url: `/testQuestions/matching/${testId}`,
        method: 'POST',
        body: data,
      }),
      invalidatesTags: (_, error) =>
        error ? [] : ['TestQuestion', 'TestQuestionIdsAndTypes'],
    }),
    updateTestQuestionMatching: builder.mutation<
      void,
      UpdateTestQuestionMatchingRequest
    >({
      query: ({ id, ...data }) => ({
        url: `/testQuestions/matching/${id}`,
        method: 'PUT',
        body: data,
      }),
      invalidatesTags: (_, error) => (error ? [] : ['TestQuestion']),
    }),
    deleteTestQuestion: builder.mutation<void, { id?: string }>({
      query: ({ id }) => ({
        url: `/testQuestions/${id}`,
        method: 'DELETE',
      }),
      invalidatesTags: (_, error) => (error ? [] : ['TestQuestionIdsAndTypes']),
    }),
  }),
});

export const {
  useGetTestQuestionQuery,
  useGetTestQuestionIdsAndTypesQuery,
  useCreateTestQuestionInputMutation,
  useUpdateTestQuestionInputMutation,
  useCreateTestQuestionSingleChoiceMutation,
  useUpdateTestQuestionSingleChoiceMutation,
  useCreateTestQuestionMultipleChoiceMutation,
  useUpdateTestQuestionMultipleChoiceMutation,
  useCreateTestQuestionMatchingMutation,
  useUpdateTestQuestionMatchingMutation,
  useDeleteTestQuestionMutation,
} = testQuestionsApi;
