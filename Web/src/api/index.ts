import { createApi } from '@reduxjs/toolkit/query/react';

import baseQueryWithReauth from '../auth/base-query-with-reauth';

export const api = createApi({
  baseQuery: baseQueryWithReauth,
  tagTypes: [
    'Session',
    'Locale',
    'Course',
    'CourseByTabId',
    'CourseList',
    'CourseMaterialAssignment',
    'CourseMaterialList',
    'CourseMemberList',
    'CourseRoles',
    'Assignment',
    'Roles',
    'Users',
  ],
  endpoints: () => ({}),
});
