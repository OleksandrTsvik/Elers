import { createApi } from '@reduxjs/toolkit/query/react';

import { baseQueryWithReauth } from '../auth';
import {
  Course,
  CourseListItem,
  CourseToEdit,
} from '../models/course.interface';
import { CourseTabType } from '../shared';

interface GetCourseByTabIdResponse {
  tabId: string;
  courseId: string;
  title: string;
  courseTabType: CourseTabType;
}

interface CreateCourseRequest {
  title: string;
  description?: string;
}

export const coursesApi = createApi({
  reducerPath: 'coursesApi',
  baseQuery: baseQueryWithReauth,
  tagTypes: ['Course', 'CourseList', 'CourseMaterialList'],
  endpoints: (builder) => ({
    getCourseById: builder.query<Course, { id?: string }>({
      query: ({ id }) => ({
        url: `/courses/${id}`,
      }),
      providesTags: ['Course'],
    }),
    getCourseByIdToEdit: builder.query<CourseToEdit, { id?: string }>({
      query: ({ id }) => ({
        url: `/courses/edit/${id}`,
      }),
      providesTags: ['Course'],
    }),
    getCourseByTabId: builder.query<GetCourseByTabIdResponse, { id?: string }>({
      query: ({ id }) => ({
        url: `/courses/tab/${id}`,
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
    updateCourseTabType: builder.mutation<
      void,
      { id: string; tabType: CourseTabType }
    >({
      query: ({ id, ...data }) => ({
        url: `/courses/tab-type/${id}`,
        method: 'PATCH',
        body: data,
      }),
      invalidatesTags: ['Course'],
    }),
    changeCourseImage: builder.mutation<
      void,
      { id: string; image: Blob; filename?: string }
    >({
      query: ({ id, image, filename }) => {
        const formData = new FormData();
        formData.append('image', image, filename);

        return {
          url: `/courses/image/${id}`,
          method: 'PATCH',
          body: formData,
        };
      },
      invalidatesTags: ['Course', 'CourseList'],
    }),
    deleteCourseImage: builder.mutation<void, { id: string }>({
      query: ({ id }) => ({
        url: `/courses/image/${id}`,
        method: 'DELETE',
      }),
      invalidatesTags: ['Course', 'CourseList'],
    }),
    deleteCourse: builder.mutation<void, { id: string }>({
      query: ({ id }) => ({
        url: `/courses/${id}`,
        method: 'DELETE',
      }),
      invalidatesTags: ['CourseList'],
    }),
  }),
});

export const {
  useGetCourseByIdQuery,
  useGetCourseByIdToEditQuery,
  useGetCourseByTabIdQuery,
  useGetListCoursesQuery,
  useCreateCourseMutation,
  useUpdateCourseTitleMutation,
  useUpdateCourseDescriptionMutation,
  useUpdateCourseTabTypeMutation,
  useChangeCourseImageMutation,
  useDeleteCourseImageMutation,
  useDeleteCourseMutation,
} = coursesApi;
