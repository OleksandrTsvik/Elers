import { lazy } from 'react';

import { RoutesType } from './routes-type.interface';
import StudentOutlet from '../outlets/student.outlet';

const MyProgressPage = lazy(
  () => import('../../pages/my-progress/my-progress.page'),
);

export const profileRoutes: RoutesType = {
  private: {
    path: 'my-progress',
    element: <StudentOutlet />,
    children: [{ index: true, element: <MyProgressPage /> }],
  },
  public: {},
};
