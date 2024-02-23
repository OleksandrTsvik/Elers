import { PayloadAction, createSlice } from '@reduxjs/toolkit';

import { User } from '../models/user.interface';
import { RootState } from '../store';

interface AuthState {
  user: User | null;
}

const initialState: AuthState = {
  user: null,
};

const authSlice = createSlice({
  name: 'authSlice',
  initialState,
  reducers: {
    setCredentials: (state, { payload }: PayloadAction<User>) => {
      state.user = payload;
    },
    logout: (state) => {
      state.user = null;
    },
  },
});

export const authReducer = authSlice.reducer;

export const { setCredentials, logout } = authSlice.actions;

export const selectAuthState = (state: RootState) => state.auth;
export const selectCurrentUser = (state: RootState) => state.auth.user;
