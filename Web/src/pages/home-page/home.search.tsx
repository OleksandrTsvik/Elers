import { Input } from 'antd';
import { useTranslation } from 'react-i18next';

import styles from './home.module.scss';

interface Props {
  onSearch: (value?: string) => void;
}

export default function HomeSearch({ onSearch }: Props) {
  const { t } = useTranslation();

  return (
    <Input.Search
      className={styles.searchInput}
      enterButton
      allowClear
      placeholder={t('home_page.search_courses')}
      onSearch={(value) => onSearch(value.length ? value : undefined)}
    />
  );
}
