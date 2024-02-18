import { PayloadAction, createSlice } from '@reduxjs/toolkit';

import { RootState } from '../store';

interface CredentialsPayload {
  user: string;
}

interface AuthState {
  user: string | null;
}

const initialState: AuthState = {
  user: null,
};

const authSlice = createSlice({
  name: 'authSlice',
  initialState,
  reducers: {
    setCredentials: (state, { payload }: PayloadAction<CredentialsPayload>) => {
      const { user } = payload;

      state.user = user;
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
