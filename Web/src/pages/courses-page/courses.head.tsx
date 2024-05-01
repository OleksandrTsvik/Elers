import { Helmet } from 'react-helmet-async';
import { useTranslation } from 'react-i18next';

export default function CoursesHead() {
  const { t } = useTranslation();

  return (
    <Helmet>
      <title>{t('courses_page.head_title')}</title>
    </Helmet>
  );
}
