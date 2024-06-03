import { RoutesType } from './routes-type.interface';
import {
  MaterialAssignmentCreationPage,
  MaterialAssignmentEditPage,
  MaterialContentCreationPage,
  MaterialContentEditPage,
  MaterialFileCreationPage,
  MaterialFileEditPage,
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
          {
            path: 'assignment/:tabId',
            element: <MaterialAssignmentCreationPage />,
          },
        ],
      },
      {
        path: 'edit',
        children: [
          { path: ':tabId/content/:id', element: <MaterialContentEditPage /> },
          { path: ':tabId/link/:id', element: <MaterialLinkEditPage /> },
          { path: ':tabId/file/:id', element: <MaterialFileEditPage /> },
          {
            path: ':tabId/assignment/:id',
            element: <MaterialAssignmentEditPage />,
          },
        ],
      },
    ],
  },
  public: {},
};
