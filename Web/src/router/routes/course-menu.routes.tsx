import { lazy } from 'react';
import { RouteObject } from 'react-router-dom';

import PrivateOutlet from '../outlets/private.outlet';

const CourseMenu = lazy(() => import('../../shared/courses/course.menu'));

const SubmittedAssignmentsPage = lazy(
  () =>
    import('../../pages/submitted-assignments-page/submitted-assignments.page'),
);

const CourseMembersPage = lazy(
  () => import('../../pages/course-members-page/course-members.page'),
);

const CoursePage = lazy(() => import('../../pages/course-page/course.page'));

export const courseMenuRoutes: RouteObject = {
  path: 'courses',
  element: <CourseMenu />,
  children: [
    { path: ':courseId', element: <CoursePage /> },
    {
      element: <PrivateOutlet />,
      children: [
        { path: 'members/:courseId', element: <CourseMembersPage /> },
        {
          path: 'submitted-assignments/:courseId',
          element: <SubmittedAssignmentsPage />,
        },
        {
          path: ':courseId/submitted-assignments/:id',
          element: <>SubmittedTaskReview</>,
        },
      ],
    },
  ],
};
