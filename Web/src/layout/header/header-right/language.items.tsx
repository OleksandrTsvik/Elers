import { MenuProps } from 'antd';

import LanguageItemIcon from './language-item.icon';
import { LocaleCode } from '../../../store/locale.slice';

type MenuItem = Required<MenuProps>['items'][number];

function getItem(language: string, code: LocaleCode): MenuItem {
  return {
    key: code,
    icon: <LanguageItemIcon language={language} code={code} />,
    label: language,
  };
}

export const languages: MenuProps['items'] = [
  getItem('Українська', 'uk'),
  getItem('English', 'us'),
];
