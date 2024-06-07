import { api } from '.';
import {
  GetCourseGradesResponse,
  GetCourseMyGradeItemResponse,
} from '../models/grade.interface';

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
        'Course',
        'Assignment',
        'CourseMaterialAssignment',
        'CourseMemberList',
      ],
    }),
  }),
});

export const { useGetCourseGradesQuery, useGetCourseMyGradesQuery } = gradesApi;
