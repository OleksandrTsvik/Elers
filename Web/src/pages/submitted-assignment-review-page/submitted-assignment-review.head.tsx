import { Helmet } from 'react-helmet-async';
import { useTranslation } from 'react-i18next';

export default function SubmittedAssignmentReviewHead() {
  const { t } = useTranslation();

  return (
    <Helmet>
      <title>{t('submitted_task_review_page.head_title')}</title>
    </Helmet>
  );
}
