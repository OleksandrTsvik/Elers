import { RoutesType } from './routes-type.interface';
import {
  AssignmentPage,
  SubmittedAssignmentsPage,
  CourseChangeImagePage,
  CourseEditPage,
  CourseMembersPage,
  CoursePage,
  CourseRolesPage,
  MyCoursesPage,
} from '../../pages';

export const coursesRoutes: RoutesType = {
  private: {
    path: 'courses',
    children: [
      { index: true, element: <MyCoursesPage /> },
      { path: 'edit/:courseId', element: <CourseEditPage /> },
      { path: 'change-image/:courseId', element: <CourseChangeImagePage /> },
      { path: 'roles/:courseId', element: <CourseRolesPage /> },
      {
        element: <CoursePage />,
        children: [
          { path: ':courseId/members', element: <CourseMembersPage /> },
          {
            path: ':courseId/submitted-assignments',
            element: <SubmittedAssignmentsPage />,
          },
        ],
      },
    ],
  },
  public: {
    path: 'courses',
    children: [
      { path: ':courseId', element: <CoursePage /> },
      {
        path: ':courseId/assignment/:id',
        element: <AssignmentPage />,
      },
    ],
  },
};
