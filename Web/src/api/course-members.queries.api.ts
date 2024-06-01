import { api } from '.';
import { CourseMember } from '../models/course-member.interface';

export const courseMembersQueriesApi = api.injectEndpoints({
  overrideExisting: false,
  endpoints: (builder) => ({
    getListCourseMembers: builder.query<CourseMember[], { courseId?: string }>({
      query: ({ courseId }) => ({
        url: `/courseMembers/${courseId}`,
      }),
      providesTags: ['Session', 'Locale', 'CourseMemberList'],
    }),
  }),
});

export const { useGetListCourseMembersQuery } = courseMembersQueriesApi;
