import { PayloadAction, createSlice } from '@reduxjs/toolkit';

import { AuthUser } from '../models/user.interface';
import { RootState } from '../store';

interface AuthState {
  user: AuthUser | null;
}

const initialState: AuthState = {
  user: null,
};

export const authSlice = createSlice({
  name: 'authSlice',
  initialState,
  reducers: {
    setCredentials: (state, { payload }: PayloadAction<AuthUser>) => {
      state.user = payload;
    },
    logout: (state) => {
      state.user = null;
    },
  },
});

export const { setCredentials, logout } = authSlice.actions;

export const selectAuthState = (state: RootState) => state.authSlice;
export const selectCurrentUser = (state: RootState) => state.authSlice.user;
