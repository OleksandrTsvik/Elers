import { HelmetProvider } from 'react-helmet-async';
import { Provider } from 'react-redux';
import { RouterProvider } from 'react-router-dom';

import AuthMiddleware from './auth/auth-middleware';
import { router } from './router/routes';
import { store } from './store';
import AntdProvider from './utils/antd/provider.antd';

import './i18n';

export default function App() {
  return (
    <Provider store={store}>
      <HelmetProvider>
        <AntdProvider>
          <AuthMiddleware>
            <RouterProvider router={router} />
          </AuthMiddleware>
        </AntdProvider>
      </HelmetProvider>
    </Provider>
  );
}
