import { api } from '.';
import { UserDto } from '../models/user.interface';

export const studentsApi = api.injectEndpoints({
  overrideExisting: false,
  endpoints: (builder) => ({
    getCourseStudents: builder.query<UserDto[], { courseId?: string }>({
      query: ({ courseId }) => ({
        url: `/students/course/${courseId}`,
      }),
      providesTags: ['Session', 'CourseMemberList'],
    }),
  }),
});

export const { useGetCourseStudentsQuery } = studentsApi;
