import { Helmet } from 'react-helmet-async';
import { useTranslation } from 'react-i18next';

export default function CourseRolesHead() {
  const { t } = useTranslation();

  return (
    <Helmet>
      <title>{t('course_roles_page.head_title')}</title>
    </Helmet>
  );
}
