import { api } from '.';
import {
  GetCourseGradesResponse,
  GetCourseMyGradeItemResponse,
  GradeType,
} from '../models/grade.interface';

interface CreateGreadRequest {
  studentId: string;
  assessmentId: string;
  gradeType: GradeType;
  value: number;
}

interface UpdateGreadRequest {
  gradeId: string;
  value: number;
}

export const gradesApi = api.injectEndpoints({
  overrideExisting: false,
  endpoints: (builder) => ({
    getCourseGrades: builder.query<
      GetCourseGradesResponse,
      { courseId?: string }
    >({
      query: ({ courseId }) => ({
        url: `/grades/course/${courseId}`,
      }),
      providesTags: [
        'Session',
        'Grades',
        'Course',
        'Assignment',
        'CourseMaterialAssignment',
        'CourseMemberList',
      ],
    }),
    getCourseMyGrades: builder.query<
      GetCourseMyGradeItemResponse[],
      { courseId?: string }
    >({
      query: ({ courseId }) => ({
        url: `/grades/course/my/${courseId}`,
      }),
      providesTags: [
        'Session',
        'Grades',
        'Course',
        'Assignment',
        'Test',
        'CourseMaterialAssignment',
        'CourseMemberList',
      ],
    }),
    createGread: builder.mutation<void, CreateGreadRequest>({
      query: (data) => ({
        url: '/grades',
        method: 'POST',
        body: data,
      }),
      invalidatesTags: (_, error) => (error ? [] : ['Grades']),
    }),
    updateGread: builder.mutation<void, UpdateGreadRequest>({
      query: ({ gradeId, ...data }) => ({
        url: `/grades/${gradeId}`,
        method: 'PUT',
        body: data,
      }),
      invalidatesTags: (_, error) => (error ? [] : ['Grades']),
    }),
  }),
});

export const {
  useGetCourseGradesQuery,
  useGetCourseMyGradesQuery,
  useCreateGreadMutation,
  useUpdateGreadMutation,
} = gradesApi;
