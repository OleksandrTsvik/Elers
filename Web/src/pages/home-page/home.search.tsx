import { Input } from 'antd';
import { useTranslation } from 'react-i18next';

export default function HomeSearch() {
  const { t } = useTranslation();

  return (
    <Input.Search
      enterButton
      placeholder={t('home_page.search_courses')}
      style={{ marginBottom: 18 }}
    />
  );
}
