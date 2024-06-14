import { colorModeSlice } from './color-mode.slice';
import { localeSlice } from './locale.slice';
import { api } from '../api';
import { authSlice } from '../auth';
import { courseEditSlice } from '../pages/course-edit-page/course-edit.slice';
import { courseGradesSlice } from '../pages/course-grades-page/course-grades.slice';
import { courseMembersSlice } from '../pages/course-members-page/course-members.slice';

const rootReducer = {
  [api.reducerPath]: api.reducer,
  [authSlice.name]: authSlice.reducer,
  [colorModeSlice.name]: colorModeSlice.reducer,
  [localeSlice.name]: localeSlice.reducer,
  [courseEditSlice.name]: courseEditSlice.reducer,
  [courseMembersSlice.name]: courseMembersSlice.reducer,
  [courseGradesSlice.name]: courseGradesSlice.reducer,
};

export default rootReducer;
