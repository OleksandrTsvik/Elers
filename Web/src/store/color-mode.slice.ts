import { PayloadAction, createSlice } from '@reduxjs/toolkit';

import { RootState } from '.';
import { COLOR_MODE } from '../utils/constants/local-storage.constants';

export type ColorMode = 'light' | 'dark';

function initColorMode(): ColorMode {
  const antdColorMode = localStorage.getItem(COLOR_MODE);
  const isDarkColorMode = matchMedia('(prefers-color-scheme: dark)').matches;

  if (antdColorMode === 'light' || antdColorMode === 'dark') {
    return antdColorMode;
  }

  if (isDarkColorMode) {
    return 'dark';
  }

  return 'light';
}

interface ColorModeState {
  mode: ColorMode;
}

const initialState: ColorModeState = {
  mode: initColorMode(),
};

export const colorModeSlice = createSlice({
  name: 'colorModeSlice',
  initialState,
  reducers: {
    setColorMode: (state, { payload }: PayloadAction<ColorMode>) => {
      state.mode = payload;
      localStorage.setItem(COLOR_MODE, payload);
    },
    toogleColorMode: (state) => {
      let newColorMode: ColorMode = 'light';

      if (state.mode === 'light') {
        newColorMode = 'dark';
      }

      state.mode = newColorMode;
      localStorage.setItem(COLOR_MODE, newColorMode);
    },
  },
});

export const { setColorMode, toogleColorMode } = colorModeSlice.actions;

export const selectColorMode = (state: RootState) => state.colorModeSlice.mode;
