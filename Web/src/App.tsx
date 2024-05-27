import { HelmetProvider } from 'react-helmet-async';
import { Provider } from 'react-redux';
import { RouterProvider } from 'react-router-dom';

import AuthMiddleware from './auth/auth.middleware';
import I18nProvider from './i18n/i18n.provider';
import { router } from './router/router';
import { store } from './store';
import AntdProvider from './utils/antd/antd.provider';
import './utils/ckeditor/ckeditor.provider';

import 'cropperjs/dist/cropper.css';

export default function App() {
  return (
    <Provider store={store}>
      <HelmetProvider>
        <AntdProvider>
          <I18nProvider>
            <AuthMiddleware>
              <RouterProvider router={router} />
            </AuthMiddleware>
          </I18nProvider>
        </AntdProvider>
      </HelmetProvider>
    </Provider>
  );
}
