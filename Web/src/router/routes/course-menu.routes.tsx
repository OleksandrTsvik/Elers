import { lazy } from 'react';
import { RouteObject } from 'react-router-dom';

import PrivateOutlet from '../outlets/private.outlet';

const CourseMenu = lazy(() => import('../../shared/courses/course.menu'));

const CoursePage = lazy(() => import('../../pages/course-page/course.page'));

const CourseMembersPage = lazy(
  () => import('../../pages/course-members-page/course-members.page'),
);

const SubmittedAssignmentsPage = lazy(
  () =>
    import('../../pages/submitted-assignments-page/submitted-assignments.page'),
);

const SubmittedAssignmentReviewPage = lazy(
  () =>
    import(
      '../../pages/submitted-assignment-review-page/submitted-assignment-review.page'
    ),
);

const CourseGradesPage = lazy(
  () => import('../../pages/course-grades-page/course-grades.page'),
);

const CourseMyGradesPage = lazy(
  () => import('../../pages/course-my-grades-page/course-my-grades.page'),
);

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
          element: <SubmittedAssignmentReviewPage />,
        },
        { path: 'grades/:courseId', element: <CourseGradesPage /> },
        { path: 'my-grades/:courseId', element: <CourseMyGradesPage /> },
      ],
    },
  ],
};
