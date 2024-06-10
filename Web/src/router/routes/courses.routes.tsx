import { lazy } from 'react';

import { RoutesType } from './routes-type.interface';

const AssignmentPage = lazy(
  () => import('../../pages/assignment-page/assignment.page'),
);

const CourseChangeImagePage = lazy(
  () => import('../../pages/course-change-image-page/course-change-image.page'),
);

const CourseEditPage = lazy(
  () => import('../../pages/course-edit-page/course-edit.page'),
);

const CourseRolesPage = lazy(
  () => import('../../pages/course-roles-page/course-roles.page'),
);

const MyCoursesPage = lazy(
  () => import('../../pages/my-courses-page/my-courses.page'),
);

const TestPage = lazy(() => import('../../pages/test-page/test.page'));

const TestPassingPage = lazy(
  () => import('../../pages/test-passing-page/test-passing.page'),
);

export const coursesRoutes: RoutesType = {
  private: {
    path: 'courses',
    children: [
      { index: true, element: <MyCoursesPage /> },
      { path: 'edit/:courseId', element: <CourseEditPage /> },
      { path: 'change-image/:courseId', element: <CourseChangeImagePage /> },
      { path: 'roles/:courseId', element: <CourseRolesPage /> },
      {
        path: ':courseId/test/attempt/:testSessionId',
        element: <TestPassingPage />,
      },
    ],
  },
  public: {
    path: 'courses',
    children: [
      {
        path: ':courseId/assignment/:id',
        element: <AssignmentPage />,
      },
      {
        path: ':courseId/test/:id',
        element: <TestPage />,
      },
    ],
  },
};
