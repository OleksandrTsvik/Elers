import { colorModeSlice } from './color-mode.slice';
import { localeSlice } from './locale.slice';
import { api } from '../api';
import { authSlice } from '../auth';
import { courseEditSlice } from '../pages/course-edit-page/course-edit.slice';

const rootReducer = {
  [api.reducerPath]: api.reducer,
  [authSlice.name]: authSlice.reducer,
  [colorModeSlice.name]: colorModeSlice.reducer,
  [localeSlice.name]: localeSlice.reducer,
  [courseEditSlice.name]: courseEditSlice.reducer,
};

export default rootReducer;
