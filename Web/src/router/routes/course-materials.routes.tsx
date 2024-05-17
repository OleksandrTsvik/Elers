import { RoutesType } from './routes-type.interface';
import { MaterialContentCreationPage } from '../../pages';

export const courseMaterialsRoutes: RoutesType = {
  private: {
    path: 'courses/material',
    children: [
      {
        path: 'add',
        children: [
          { path: 'content/:tabId', element: <MaterialContentCreationPage /> },
        ],
      },
      {
        path: 'edit',
        children: [],
      },
    ],
  },
  public: {},
};
