import { Helmet } from 'react-helmet-async';
import { useTranslation } from 'react-i18next';

export default function CourseMembersHead() {
  const { t } = useTranslation();

  return (
    <Helmet>
      <title>{t('course_members_page.head_title')}</title>
    </Helmet>
  );
}
