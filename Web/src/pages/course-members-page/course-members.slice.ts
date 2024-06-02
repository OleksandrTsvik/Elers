import { PayloadAction, createSlice } from '@reduxjs/toolkit';

import { CourseMember } from '../../models/course-member.interface';
import { RootState } from '../../store';

interface CourseMembersState {
  openMemberRoleModal: boolean;
  currentCourseMember?: CourseMember;
}

const initialState: CourseMembersState = { openMemberRoleModal: false };

export const courseMembersSlice = createSlice({
  name: 'courseMembersSlice',
  initialState,
  reducers: {
    openRoleModal: (state, { payload }: PayloadAction<CourseMember>) => {
      state.currentCourseMember = payload;
      state.openMemberRoleModal = true;
    },
    closeRoleModal: (state) => {
      state.currentCourseMember = undefined;
      state.openMemberRoleModal = false;
    },
  },
});

export const { openRoleModal, closeRoleModal } = courseMembersSlice.actions;

export const selectCourseMembersState = (state: RootState) =>
  state.courseMembersSlice;
