import { PayloadAction, createSlice } from '@reduxjs/toolkit';

import { CourseTabModalMode } from './modals/tab-modal-mode.enum';
import { CourseTab } from '../../models/course.interface';
import { RootState } from '../../store';

interface CourseEditState {
  activeCourseTab?: CourseTab;
  modalMode?: CourseTabModalMode;
}

const initialState: CourseEditState = {};

const courseEditSlice = createSlice({
  name: 'courseEditSlice',
  initialState,
  reducers: {
    setActiveCourseTab: (
      state,
      { payload }: PayloadAction<CourseTab | undefined>,
    ) => {
      state.activeCourseTab = payload;
    },
    setModalMode: (
      state,
      { payload }: PayloadAction<CourseTabModalMode | undefined>,
    ) => {
      state.modalMode = payload;
    },
  },
});

export const courseEditReducer = courseEditSlice.reducer;

export const { setActiveCourseTab, setModalMode } = courseEditSlice.actions;

export const selectCourseEditState = (state: RootState) => state.courseEdit;
