import { createApi } from '@reduxjs/toolkit/query/react';

import { baseQueryWithReauth } from '../auth/base-query-with-reauth';
import { Course } from '../models/course.interface';

interface CreateCourseRequest {
  title: string;
  description: string;
}

export const coursesApi = createApi({
  reducerPath: 'coursesApi',
  baseQuery: baseQueryWithReauth,
  tagTypes: ['Courses'],
  endpoints: (builder) => ({
    getListCourses: builder.query<Course[], void>({
      query: () => ({
        url: '/courses',
      }),
      providesTags: ['Courses'],
    }),
    createCourse: builder.mutation<void, CreateCourseRequest>({
      query: (data) => ({
        url: '/courses',
        method: 'POST',
        body: data,
      }),
      invalidatesTags: ['Courses'],
    }),
  }),
});

export const { useGetListCoursesQuery, useCreateCourseMutation } = coursesApi;
