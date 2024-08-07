import { PayloadAction, createSlice } from '@reduxjs/toolkit';

import { RootState } from '.';
import { LOCALE_CODE } from '../utils/constants/local-storage.constants';

export type LocaleCode = 'uk' | 'en';

export const DEFAULT_LOCALE: LocaleCode = 'uk';

function initLocale(): LocaleCode {
  const locale = localStorage.getItem(LOCALE_CODE);

  if (locale === 'uk' || locale === 'en') {
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

export const localeSlice = createSlice({
  name: 'localeSlice',
  initialState,
  reducers: {
    setLocale: (state, { payload }: PayloadAction<LocaleCode>) => {
      state.locale = payload;
      localStorage.setItem(LOCALE_CODE, payload);
    },
  },
});

export const { setLocale } = localeSlice.actions;

export const selectLocale = (state: RootState) => state.localeSlice.locale;
