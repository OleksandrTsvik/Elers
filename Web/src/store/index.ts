import { Middleware, Reducer, configureStore } from '@reduxjs/toolkit';

import { colorModeReducer } from './color-mode.slice';
import { localeReducer } from './locale.slice';
import { apiReducers } from '../api';
import { authReducer } from '../auth/auth.slice';
import { IS_DEVELOPMENT } from '../utils/constants/node-env.constants';

const apiReducersObj = apiReducers.reduce(
  (obj, api) => {
    obj[api.reducerPath] = api.reducer;

    return obj;
  },
  {} as { [key: string]: Reducer },
);

const apiMiddlewares: Middleware[] = apiReducers.map((api) => api.middleware);

export const store = configureStore({
  reducer: {
    auth: authReducer,
    locale: localeReducer,
    colorMode: colorModeReducer,
    ...apiReducersObj,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(apiMiddlewares),
  devTools: IS_DEVELOPMENT,
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
