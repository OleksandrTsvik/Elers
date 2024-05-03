import { RoutesType } from './routes-type.interface';
import { CoursePage, MyCoursesPage } from '../../pages';

export const coursesRoutes: RoutesType = {
  private: {
    path: 'courses',
    children: [{ index: true, element: <MyCoursesPage /> }],
  },
  public: {
    path: 'courses',
    children: [{ path: ':courseId', element: <CoursePage /> }],
  },
};
