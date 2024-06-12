import { api } from '.';
import { PagedList, PagingParams } from '../common/types';
import {
  Course,
  CourseListItem,
  CourseToEdit,
  MyCourseListItem,
} from '../models/course.interface';
import { CourseTabType } from '../shared';

interface GetCourseByTabIdResponse {
  tabId: string;
  courseId: string;
  title: string;
  courseTabType: CourseTabType;
}

interface GetListCoursesRequest extends PagingParams {
  search?: string;
}

interface GetMyCoursesRequest extends PagingParams {}

interface CreateCourseRequest {
  title: string;
  description?: string;
}

export const coursesApi = api.injectEndpoints({
  overrideExisting: false,
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
      providesTags: ['Session', 'Course'],
    }),
    getCourseByTabId: builder.query<GetCourseByTabIdResponse, { id?: string }>({
      query: ({ id }) => ({
        url: `/courses/tab/${id}`,
      }),
      providesTags: ['CourseByTabId'],
    }),
    getListCourses: builder.query<
      PagedList<CourseListItem>,
      GetListCoursesRequest
    >({
      query: (params) => ({
        url: '/courses',
        params,
      }),
      providesTags: ['CourseList', 'CourseMaterialList'],
    }),
    getMyCourses: builder.query<
      PagedList<MyCourseListItem>,
      GetMyCoursesRequest
    >({
      query: (params) => ({
        url: '/courses/my',
        params,
      }),
      providesTags: ['Session', 'CourseList', 'MyCourses'],
    }),
    createCourse: builder.mutation<void, CreateCourseRequest>({
      query: (data) => ({
        url: '/courses',
        method: 'POST',
        body: data,
      }),
      invalidatesTags: (_, error) => (error ? [] : ['CourseList']),
    }),
    updateCourseTitle: builder.mutation<void, { id: string; title: string }>({
      query: ({ id, ...data }) => ({
        url: `/courses/title/${id}`,
        method: 'PATCH',
        body: data,
      }),
      invalidatesTags: (_, error) =>
        error ? [] : ['Course', 'CourseByTabId', 'CourseList'],
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
      invalidatesTags: (_, error) => (error ? [] : ['Course', 'CourseList']),
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
      invalidatesTags: (_, error) => (error ? [] : ['Course', 'CourseByTabId']),
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
      invalidatesTags: (_, error) => (error ? [] : ['Course', 'CourseList']),
    }),
    deleteCourseImage: builder.mutation<void, { id: string }>({
      query: ({ id }) => ({
        url: `/courses/image/${id}`,
        method: 'DELETE',
      }),
      invalidatesTags: (_, error) => (error ? [] : ['Course', 'CourseList']),
    }),
    deleteCourse: builder.mutation<void, { id: string }>({
      query: ({ id }) => ({
        url: `/courses/${id}`,
        method: 'DELETE',
      }),
      invalidatesTags: (_, error) => (error ? [] : ['CourseList']),
    }),
  }),
});

export const {
  useGetCourseByIdQuery,
  useGetCourseByIdToEditQuery,
  useGetCourseByTabIdQuery,
  useGetListCoursesQuery,
  useGetMyCoursesQuery,
  useCreateCourseMutation,
  useUpdateCourseTitleMutation,
  useUpdateCourseDescriptionMutation,
  useUpdateCourseTabTypeMutation,
  useChangeCourseImageMutation,
  useDeleteCourseImageMutation,
  useDeleteCourseMutation,
} = coursesApi;
