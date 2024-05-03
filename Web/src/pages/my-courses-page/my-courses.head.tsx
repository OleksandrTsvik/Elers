import { Helmet } from 'react-helmet-async';
import { useTranslation } from 'react-i18next';

export default function MyCoursesHead() {
  const { t } = useTranslation();

  return (
    <Helmet>
      <title>{t('my_courses_page.head_title')}</title>
    </Helmet>
  );
}
