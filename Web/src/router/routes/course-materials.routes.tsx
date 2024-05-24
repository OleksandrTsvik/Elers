import { RoutesType } from './routes-type.interface';
import {
  MaterialContentCreationPage,
  MaterialContentEditPage,
  MaterialFileCreationPage,
  MaterialLinkCreationPage,
  MaterialLinkEditPage,
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
          { path: 'file/:tabId', element: <MaterialFileCreationPage /> },
        ],
      },
      {
        path: 'edit',
        children: [
          { path: ':tabId/content/:id', element: <MaterialContentEditPage /> },
          { path: ':tabId/link/:id', element: <MaterialLinkEditPage /> },
        ],
      },
    ],
  },
  public: {},
};
