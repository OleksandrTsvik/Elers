import { MenuProps } from 'antd';

import LanguageItemIcon from './language-item.icon';
import { LocaleCode } from '../../../store/locale.slice';

type MenuItem = Required<MenuProps>['items'][number];

export const languages: MenuProps['items'] = [
  getItem('Українська', 'uk'),
  getItem('English', 'en'),
];

type LocaleTitles = { [key in LocaleCode]: string };

export const localeTitles: LocaleTitles = {
  uk: 'УКР',
  en: 'EN',
};

function getItem(language: string, code: LocaleCode): MenuItem {
  return {
    key: code,
    icon: <LanguageItemIcon language={language} code={code} />,
    label: language,
  };
}
