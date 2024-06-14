import { PayloadAction, createSlice } from '@reduxjs/toolkit';

import { GradesModalMode } from './modals/grades-modal-mode.enum';
import { AssessmentItem, GradeType } from '../../models/grade.interface';
import { UserDto } from '../../models/user.interface';
import { RootState } from '../../store';

export interface ActiveGradeCell {
  assessmentId: string;
  title: string;
  gradeType: GradeType;
  student: UserDto;
  maxGrade?: number;
  gradeId?: string;
  grade?: number;
}

interface CourseGradesState {
  activeColumn?: AssessmentItem;
  activeGradeCell?: ActiveGradeCell;
  modalMode?: GradesModalMode;
}

const initialState: CourseGradesState = {};

export const courseGradesSlice = createSlice({
  name: 'courseGradesSlice',
  initialState,
  reducers: {
    setActiveColumn: (
      state,
      { payload }: PayloadAction<AssessmentItem | undefined>,
    ) => {
      state.activeColumn = payload;
    },
    setActiveGradeCell: (
      state,
      { payload }: PayloadAction<ActiveGradeCell | undefined>,
    ) => {
      state.activeGradeCell = payload;
    },
    setModalMode: (
      state,
      { payload }: PayloadAction<GradesModalMode | undefined>,
    ) => {
      state.modalMode = payload;
    },
  },
});

export const { setActiveColumn, setActiveGradeCell, setModalMode } =
  courseGradesSlice.actions;

export const selectCourseGradesState = (state: RootState) =>
  state.courseGradesSlice;
