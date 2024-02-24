import { Button, Dropdown } from 'antd';

import { languages, localeTitles } from './language.items';
import useLocale from '../../../hooks/use-locale';
import { LocaleCode } from '../../../store/locale.slice';

import styles from './language.module.scss';

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
      <Button className={styles.languageButton} type="text">
        {localeTitles[locale]}
      </Button>
    </Dropdown>
  );
}
