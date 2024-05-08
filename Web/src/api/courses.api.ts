import { createApi } from '@reduxjs/toolkit/query/react';

import { baseQueryWithReauth } from '../auth/base-query-with-reauth';
import { Course, CourseListItem } from '../models/course.interface';

interface CreateCourseRequest {
  title: string;
  description: string;
}

export const coursesApi = createApi({
  reducerPath: 'coursesApi',
  baseQuery: baseQueryWithReauth,
  tagTypes: ['Course', 'CourseList'],
  endpoints: (builder) => ({
    getCourseById: builder.query<Course, { id?: string }>({
      query: ({ id }) => ({
        url: `/courses/${id}`,
      }),
      providesTags: ['Course'],
    }),
    getListCourses: builder.query<CourseListItem[], void>({
      query: () => ({
        url: '/courses',
      }),
      providesTags: ['CourseList'],
    }),
    createCourse: builder.mutation<void, CreateCourseRequest>({
      query: (data) => ({
        url: '/courses',
        method: 'POST',
        body: data,
      }),
      invalidatesTags: ['CourseList'],
    }),
    updateCourseTitle: builder.mutation<void, { id: string; title: string }>({
      query: ({ id, ...data }) => ({
        url: `/courses/title/${id}`,
        method: 'PATCH',
        body: data,
      }),
      invalidatesTags: ['Course', 'CourseList'],
    }),
    updateCourseDescription: builder.mutation<
      void,
      { id: string; description?: string }
    >({
      query: ({ id, ...data }) => ({
        url: `/courses/description/${id}`,
        method: 'PATCH',
        body: data,
      }),
      invalidatesTags: ['Course', 'CourseList'],
    }),
  }),
});

export const {
  useGetCourseByIdQuery,
  useGetListCoursesQuery,
  useCreateCourseMutation,
  useUpdateCourseTitleMutation,
  useUpdateCourseDescriptionMutation,
} = coursesApi;
