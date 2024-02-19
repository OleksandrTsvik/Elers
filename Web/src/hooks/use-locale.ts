import { useTranslation } from 'react-i18next';

import { useAppDispatch, useAppSelector } from './redux-hooks';
import {
  LocaleCode,
  selectLocale,
  setLocale as setLocaleCode,
} from '../store/locale.slice';

export default function useLocale() {
  const { i18n } = useTranslation();
  const appDispatch = useAppDispatch();

  const locale = useAppSelector(selectLocale);

  const setLocale = (code: LocaleCode) => {
    appDispatch(setLocaleCode(code));

    void i18n.changeLanguage(code);
  };

  return { locale, setLocale };
}
