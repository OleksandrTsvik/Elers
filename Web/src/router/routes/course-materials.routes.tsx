import { RoutesType } from './routes-type.interface';
import {
  MaterialContentCreationPage,
  MaterialContentEditPage,
  MaterialLinkCreationPage,
} from '../../pages';

export const courseMaterialsRoutes: RoutesType = {
  private: {
    path: 'courses/material',
    children: [
      {
        path: 'add',
        children: [
          { path: 'content/:tabId', element: <MaterialContentCreationPage /> },
          { path: 'link/:tabId', element: <MaterialLinkCreationPage /> },
        ],
      },
      {
        path: 'edit',
        children: [
          { path: ':tabId/content/:id', element: <MaterialContentEditPage /> },
        ],
      },
    ],
  },
  public: {},
};
