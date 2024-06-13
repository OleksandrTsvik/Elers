import { api } from '.';

export const profileApi = api.injectEndpoints({
  overrideExisting: false,
  endpoints: (builder) => ({
    getMyEnrolledCourses: builder.query<{ id: string; title: string }[], void>({
      query: () => ({
        url: '/profile/enrolled-courses',
      }),
      providesTags: ['Session', 'CourseList', 'MyCourses'],
    }),
  }),
});

export const { useGetMyEnrolledCoursesQuery } = profileApi;
