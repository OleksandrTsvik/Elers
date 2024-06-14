import { lazy } from 'react';

import { RoutesType } from './routes-type.interface';
import StudentOutlet from '../outlets/student.outlet';

const MyProgressPage = lazy(
  () => import('../../pages/my-progress/my-progress.page'),
);

const MyProfilePage = lazy(
  () => import('../../pages/my-profile-page/my-profile.page'),
);

export const profileRoutes: RoutesType = {
  private: {
    children: [
      {
        path: 'my-progress',
        element: <StudentOutlet />,
        children: [{ index: true, element: <MyProgressPage /> }],
      },
      {
        path: 'profile',
        element: <MyProfilePage />,
      },
    ],
  },
  public: {},
};
