import { PayloadAction, createSlice } from '@reduxjs/toolkit';

import { CourseEditModalMode } from './modals/edit-modal-mode.enum';
import { CourseMaterial } from '../../models/course-material.type';
import { CourseTab } from '../../models/course-tab.interface';
import { RootState } from '../../store';

interface CourseEditState {
  activeCourseTab?: CourseTab;
  activeCourseMaterial?: CourseMaterial;
  modalMode?: CourseEditModalMode;
}

const initialState: CourseEditState = {};

export const courseEditSlice = createSlice({
  name: 'courseEditSlice',
  initialState,
  reducers: {
    setActiveCourseTab: (
      state,
      { payload }: PayloadAction<CourseTab | undefined>,
    ) => {
      state.activeCourseTab = payload;
    },
    setActiveCourseMaterial: (
      state,
      { payload }: PayloadAction<CourseMaterial | undefined>,
    ) => {
      state.activeCourseMaterial = payload;
    },
    setModalMode: (
      state,
      { payload }: PayloadAction<CourseEditModalMode | undefined>,
    ) => {
      state.modalMode = payload;
    },
  },
});

export const { setActiveCourseTab, setActiveCourseMaterial, setModalMode } =
  courseEditSlice.actions;

export const selectCourseEditState = (state: RootState) =>
  state.courseEditSlice;
