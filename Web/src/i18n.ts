import * as i18n from 'i18next';
import { initReactI18next } from 'react-i18next';

import { DEFAULT_LOCALE } from './store/locale.slice';
import { LOCALE_CODE } from './utils/constants/local-storage.constants';

import translationEN from './locales/en/translation.json';
import translationUK from './locales/uk/translation.json';

const resources: i18n.Resource = {
  en: { translation: translationEN },
  uk: { translation: translationUK },
};

await i18n.use(initReactI18next).init({
  resources,
  lng: localStorage.getItem(LOCALE_CODE) || DEFAULT_LOCALE,

  interpolation: {
    escapeValue: false,
  },
});

export default i18n;
