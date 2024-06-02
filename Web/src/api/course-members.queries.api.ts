import { api } from '.';
import { PagedList, PagingParams, SortParams } from '../common/types';
import { CourseMember } from '../models/course-member.interface';

interface GetListCourseMembersRequest extends PagingParams, SortParams {
  courseId?: string;
  firstName?: string;
  lastName?: string;
  patronymic?: string;
}

export const courseMembersQueriesApi = api.injectEndpoints({
  overrideExisting: false,
  endpoints: (builder) => ({
    getListCourseMembers: builder.query<
      PagedList<CourseMember>,
      GetListCourseMembersRequest
    >({
      query: ({ courseId, ...params }) => ({
        url: `/courseMembers/${courseId}`,
        params,
      }),
      providesTags: ['Session', 'Locale', 'CourseMemberList'],
    }),
  }),
});

export const { useGetListCourseMembersQuery } = courseMembersQueriesApi;
