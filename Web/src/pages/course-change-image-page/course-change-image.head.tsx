import { Helmet } from 'react-helmet-async';
import { useTranslation } from 'react-i18next';

export default function CourseChangeImageHead() {
  const { t } = useTranslation();

  return (
    <Helmet>
      <title>{t('course_change_image_page.head_title')}</title>
    </Helmet>
  );
}
