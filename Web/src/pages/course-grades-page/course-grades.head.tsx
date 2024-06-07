import { Helmet } from 'react-helmet-async';
import { useTranslation } from 'react-i18next';

export default function CourseGradesHead() {
  const { t } = useTranslation();

  return (
    <Helmet>
      <title>{t('course.grades')}</title>
    </Helmet>
  );
}
