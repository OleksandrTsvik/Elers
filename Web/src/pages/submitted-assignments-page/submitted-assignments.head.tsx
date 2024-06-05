import { Helmet } from 'react-helmet-async';
import { useTranslation } from 'react-i18next';

export default function SubmittedAssignmentsHead() {
  const { t } = useTranslation();

  return (
    <Helmet>
      <title>{t('course.submitted_assignment')}</title>
    </Helmet>
  );
}
