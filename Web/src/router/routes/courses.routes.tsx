import { RoutesType } from './routes-type.interface';
import { CoursePage, CoursesPage } from '../../pages';

export const coursesRoutes: RoutesType = {
  private: {
    path: 'courses',
    children: [{ index: true, element: <CoursesPage /> }],
  },
  public: {
    path: 'courses',
    children: [{ path: ':courseId', element: <CoursePage /> }],
  },
};
