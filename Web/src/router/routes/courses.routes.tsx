import { RoutesType } from './routes-type.interface';
import { CourseEditPage, CoursePage, MyCoursesPage } from '../../pages';

export const coursesRoutes: RoutesType = {
  private: {
    path: 'courses',
    children: [
      { index: true, element: <MyCoursesPage /> },
      { path: 'edit/:courseId', element: <CourseEditPage /> },
    ],
  },
  public: {
    path: 'courses',
    children: [{ path: ':courseId', element: <CoursePage /> }],
  },
};
