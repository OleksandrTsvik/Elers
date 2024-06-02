import { RoutesType } from './routes-type.interface';
import {
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
      { path: 'members/:courseId', element: <CourseMembersPage /> },
    ],
  },
  public: {
    path: 'courses',
    children: [{ path: ':courseId', element: <CoursePage /> }],
  },
};
