import { createApi } from '@reduxjs/toolkit/query/react';

import { baseQueryWithReauth } from '../auth/base-query-with-reauth';
import { Course, CourseListItem, CourseTab } from '../models/course.interface';

interface CreateCourseRequest {
  title: string;
  description: string;
}

interface CreateCourseTabRequest {
  courseId: string;
  name: string;
}

export const coursesApi = createApi({
  reducerPath: 'coursesApi',
  baseQuery: baseQueryWithReauth,
  tagTypes: ['Course', 'Courses', 'CourseToEdit'],
  endpoints: (builder) => ({
    getCourseById: builder.query<Course, { id?: string }>({
      query: ({ id }) => ({
        url: `/courses/${id}`,
      }),
      providesTags: ['Course'],
    }),
    getCourseByIdToEdit: builder.query<Course, { id?: string }>({
      query: ({ id }) => ({
        url: `/courses/${id}`,
      }),
      providesTags: ['CourseToEdit'],
    }),
    getListCourses: builder.query<CourseListItem[], void>({
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
    updateCourseTitle: builder.mutation<void, { id: string; title: string }>({
      query: ({ id, ...data }) => ({
        url: `/courses/title/${id}`,
        method: 'PATCH',
        body: data,
      }),
      invalidatesTags: ['Course', 'Courses'],
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
      invalidatesTags: ['Course', 'Courses'],
    }),
    createCourseTab: builder.mutation<CourseTab, CreateCourseTabRequest>({
      query: ({ courseId, ...data }) => ({
        url: `/courseTabs/${courseId}`,
        method: 'POST',
        body: data,
      }),
      invalidatesTags: ['Course'],
    }),
    deleteCourseTab: builder.mutation<void, { id: string }>({
      query: ({ id }) => ({
        url: `/courseTabs/${id}`,
        method: 'DELETE',
      }),
      invalidatesTags: ['Course'],
    }),
  }),
});

export const {
  useGetCourseByIdQuery,
  useGetCourseByIdToEditQuery,
  useGetListCoursesQuery,
  useCreateCourseMutation,
  useUpdateCourseTitleMutation,
  useUpdateCourseDescriptionMutation,
  useCreateCourseTabMutation,
  useDeleteCourseTabMutation,
} = coursesApi;
