import { api } from '.';
import {
  Test,
  TestSession,
  TestSessionQuestion,
} from '../models/test.interface';

interface SendAnswerToTestQuestionRequest {
  testSessionId?: string;
  questionId: string;
  answer?: string;
  answers?: string[];
}

export const testsApi = api.injectEndpoints({
  overrideExisting: false,
  endpoints: (builder) => ({
    getTest: builder.query<Test, { id?: string }>({
      query: ({ id }) => ({
        url: `/tests/${id}`,
      }),
      providesTags: ['Session', 'Test', 'CourseMaterialTest', 'TestQuestion'],
    }),
    getTestSession: builder.query<TestSession, { id?: string }>({
      query: ({ id }) => ({
        url: `/tests/session/${id}`,
      }),
      providesTags: [
        'Session',
        'Test',
        'TestSession',
        'CourseMaterialTest',
        'TestQuestion',
      ],
    }),
    getTestSessionQuestion: builder.query<
      TestSessionQuestion,
      { testSessionId: string; questionId: string }
    >({
      query: ({ testSessionId, questionId }) => ({
        url: `/tests/${testSessionId}/${questionId}`,
      }),
      providesTags: [
        'Session',
        'Test',
        'TestSession',
        'CourseMaterialTest',
        'TestQuestion',
      ],
    }),
    startTest: builder.mutation<string, { testId: string }>({
      query: ({ testId }) => ({
        url: `/tests/${testId}`,
        method: 'POST',
      }),
      invalidatesTags: (_, error) => (error ? [] : ['Test', 'TestSession']),
    }),
    sendAnswerToTestQuestion: builder.mutation<
      void,
      SendAnswerToTestQuestionRequest
    >({
      query: ({ testSessionId, questionId, ...data }) => ({
        url: `/tests/${testSessionId}/${questionId}`,
        method: 'POST',
        body: data,
      }),
      invalidatesTags: (_, error) => (error ? [] : ['TestSession']),
    }),
    finishTest: builder.mutation<void, { testSessionId: string }>({
      query: ({ testSessionId }) => ({
        url: `/tests/finish/${testSessionId}`,
        method: 'POST',
      }),
      invalidatesTags: (_, error) => (error ? [] : ['Test']),
    }),
  }),
});

export const {
  useGetTestQuery,
  useGetTestSessionQuery,
  useGetTestSessionQuestionQuery,
  useStartTestMutation,
  useSendAnswerToTestQuestionMutation,
  useFinishTestMutation,
} = testsApi;
