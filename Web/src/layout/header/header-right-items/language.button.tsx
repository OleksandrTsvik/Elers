import Icon from '@ant-design/icons';
import { Button, Dropdown } from 'antd';
import { HiLanguage } from 'react-icons/hi2';

import { languages } from './language.items';
import useLocale from '../../../hooks/use-locale';
import { LocaleCode } from '../../../store/locale.slice';

export default function LanguageButton() {
  const { locale, setLocale } = useLocale();

  return (
    <Dropdown
      trigger={['click']}
      menu={{
        items: languages,
        selectable: true,
        defaultSelectedKeys: [locale],
        onSelect: ({ key }) => setLocale(key as LocaleCode),
      }}
    >
      <Button type="text" icon={<Icon component={HiLanguage} />} />
    </Dropdown>
  );
}
