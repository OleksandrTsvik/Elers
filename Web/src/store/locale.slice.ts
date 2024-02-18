import { PayloadAction, createSlice } from '@reduxjs/toolkit';

import { RootState } from '.';
import { LOCALE_CODE } from '../utils/constants/local-storage.constants';

export type LocaleCode = 'uk' | 'us';

const DEFAULT_LOCALE: LocaleCode = 'uk';

function initLocale(): LocaleCode {
  const locale = localStorage.getItem(LOCALE_CODE);

  if (locale === 'uk' || locale === 'us') {
    return locale;
  }

  return DEFAULT_LOCALE;
}

interface LocaleState {
  locale: LocaleCode;
}

const initialState: LocaleState = {
  locale: initLocale(),
};

const localeSlice = createSlice({
  name: 'localeSlice',
  initialState,
  reducers: {
    setLocale: (state, { payload }: PayloadAction<LocaleCode>) => {
      state.locale = payload;
      localStorage.setItem(LOCALE_CODE, payload);
    },
  },
});

export const localeReducer = localeSlice.reducer;

export const { setLocale } = localeSlice.actions;

export const selectLocale = (state: RootState) => state.locale.locale;
