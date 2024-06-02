import { useTranslation } from 'react-i18next';

import { useAppDispatch, useAppSelector } from './redux-hooks';
import { api } from '../api';
import {
  LocaleCode,
  selectLocale,
  setLocale as setLocaleCode,
} from '../store/locale.slice';

export default function useLocale() {
  const { i18n } = useTranslation();
  const appDispatch = useAppDispatch();

  const locale = useAppSelector(selectLocale);

  const setLocale = async (code: LocaleCode) => {
    appDispatch(setLocaleCode(code));

    await i18n.changeLanguage(code);

    document.documentElement.lang = code;
    document.documentElement.dir = i18n.dir(code);

    appDispatch(api.util.invalidateTags(['Locale']));
  };

  return { locale, setLocale };
}
