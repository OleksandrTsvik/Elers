import { useAppDispatch, useAppSelector } from './store';
import {
  LocaleCode,
  selectLocale,
  setLocale as setLocaleCode,
} from '../store/locale.slice';

export default function useLocale() {
  const appDispatch = useAppDispatch();

  const locale = useAppSelector(selectLocale);

  const setLocale = (code: LocaleCode) => {
    appDispatch(setLocaleCode(code));
  };

  return { locale, setLocale };
}
