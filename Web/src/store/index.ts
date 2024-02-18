import { configureStore } from '@reduxjs/toolkit';

import { colorModeReducer } from './color-mode.slice';
import { localeReducer } from './locale.slice';
import { authReducer } from '../auth/auth.slice';
import { IS_DEVELOPMENT } from '../utils/constants/node-env.constants';

export const store = configureStore({
  reducer: {
    auth: authReducer,
    locale: localeReducer,
    colorMode: colorModeReducer,
  },
  middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(),
  devTools: IS_DEVELOPMENT,
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
