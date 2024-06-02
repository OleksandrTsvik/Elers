import { configureStore } from '@reduxjs/toolkit';

import rootReducer from './reducers';
import { api } from '../api';
import { IS_DEVELOPMENT } from '../utils/constants/node-env.constants';

export const store = configureStore({
  reducer: rootReducer,
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(api.middleware),
  devTools: IS_DEVELOPMENT,
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
