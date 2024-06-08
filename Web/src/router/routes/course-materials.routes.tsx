import { lazy } from 'react';

import { RoutesType } from './routes-type.interface';

const MaterialAssignmentCreationPage = lazy(
  () =>
    import(
      '../../pages/material-assignment-creation-page/material-assignment-creation.page'
    ),
);

const MaterialAssignmentEditPage = lazy(
  () =>
    import(
      '../../pages/material-assignment-edit-page/material-assignment-edit.page'
    ),
);

const MaterialContentCreationPage = lazy(
  () =>
    import(
      '../../pages/material-content-creation-page/material-content-creation.page'
    ),
);

const MaterialContentEditPage = lazy(
  () =>
    import('../../pages/material-content-edit-page/material-content-edit.page'),
);

const MaterialFileCreationPage = lazy(
  () =>
    import(
      '../../pages/material-file-creation-page/material-file-creation.page'
    ),
);

const MaterialFileEditPage = lazy(
  () => import('../../pages/material-file-edit-page/material-file-edit.page'),
);

const MaterialLinkCreationPage = lazy(
  () =>
    import(
      '../../pages/material-link-creation-page/material-link-creation.page'
    ),
);

const MaterialLinkEditPage = lazy(
  () => import('../../pages/material-link-edit-page/material-link-edit.page'),
);

const MaterialTestCreationPage = lazy(
  () =>
    import(
      '../../pages/material-test-creation-page/material-test-creation.page'
    ),
);

const MaterialTestEditPage = lazy(
  () => import('../../pages/material-test-edit-page/material-test-edit.page'),
);

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
          { path: 'test/:tabId', element: <MaterialTestCreationPage /> },
        ],
      },
      {
        path: 'edit/:tabId',
        children: [
          { path: 'content/:id', element: <MaterialContentEditPage /> },
          { path: 'link/:id', element: <MaterialLinkEditPage /> },
          { path: 'file/:id', element: <MaterialFileEditPage /> },
          { path: 'assignment/:id', element: <MaterialAssignmentEditPage /> },
          { path: 'test/:id', element: <MaterialTestEditPage /> },
        ],
      },
    ],
  },
  public: {},
};
