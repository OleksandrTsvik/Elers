import { Middleware, configureStore } from '@reduxjs/toolkit';

import rootReducer from './reducers';
import { apiReducers } from '../api';
import { IS_DEVELOPMENT } from '../utils/constants/node-env.constants';

const apiMiddlewares: Middleware[] = apiReducers.map((api) => api.middleware);

export const store = configureStore({
  reducer: rootReducer,
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(apiMiddlewares),
  devTools: IS_DEVELOPMENT,
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
