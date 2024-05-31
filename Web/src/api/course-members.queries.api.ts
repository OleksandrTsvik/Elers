import { courseMembersApi } from './course-members.mutations.api';
import { CourseMember } from '../models/course-member.interface';

export const courseMembersQueriesApi = courseMembersApi.injectEndpoints({
  overrideExisting: false,
  endpoints: (builder) => ({
    getListCourseMembers: builder.query<CourseMember[], { courseId?: string }>({
      query: ({ courseId }) => ({
        url: `/courseMembers/${courseId}`,
      }),
    }),
  }),
});

export const { useGetListCourseMembersQuery } = courseMembersQueriesApi;
