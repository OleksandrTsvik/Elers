import { ConfigProviderProps } from 'antd';
import { DirectionType } from 'antd/es/config-provider';

import enUS from 'antd/locale/en_US';
import ukUA from 'antd/locale/uk_UA';

import { LocaleCode } from '../../store/locale.slice';

import 'dayjs/locale/uk';

export type Locale = ConfigProviderProps['locale'];

export interface LocaleSettings {
  antd: Locale;
  dayjs: string;
  direction?: DirectionType;
}

export type Locales = { [key in LocaleCode]: LocaleSettings };

// all supported locales for antd
// https://ant.design/docs/react/i18n

// all supported locales for dayjs
// https://day.js.org/docs/en/i18n/i18n

export const locales: Locales = {
  uk: { antd: ukUA, dayjs: 'uk' },
  en: { antd: enUS, dayjs: 'en' },
};
