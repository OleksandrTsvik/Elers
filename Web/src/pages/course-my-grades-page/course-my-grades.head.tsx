import { Helmet } from 'react-helmet-async';
import { useTranslation } from 'react-i18next';

export default function CourseMyGradesHead() {
  const { t } = useTranslation();

  return (
    <Helmet>
      <title>{t('course.my_grades')}</title>
    </Helmet>
  );
}
