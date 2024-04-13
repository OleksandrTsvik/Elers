import * as i18n from 'i18next';
import HttpApi from 'i18next-http-backend';
import { initReactI18next } from 'react-i18next';

import { DEFAULT_LOCALE } from '../store/locale.slice';
import { LOCALE_CODE } from '../utils/constants/local-storage.constants';

await i18n
  .use(HttpApi)
  .use(initReactI18next)
  .init({
    fallbackLng: DEFAULT_LOCALE,
    lng: localStorage.getItem(LOCALE_CODE) || DEFAULT_LOCALE,
    interpolation: {
      escapeValue: false,
    },
    backend: {
      loadPath: '/locales/{{lng}}/{{ns}}.json',
    },
  });

export default i18n;
