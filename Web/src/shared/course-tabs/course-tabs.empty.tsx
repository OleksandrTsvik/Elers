import { Empty } from 'antd';
import { useTranslation } from 'react-i18next';

export function CourseTabsEmpty() {
  const { t } = useTranslation();

  return (
    <Empty
      image={Empty.PRESENTED_IMAGE_SIMPLE}
      description={t('course_edit_page.no_sections')}
    />
  );
}