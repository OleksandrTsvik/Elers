import { Helmet } from 'react-helmet-async';
import { useTranslation } from 'react-i18next';

export default function MaterialAssignmentEditHead() {
  const { t } = useTranslation();

  return (
    <Helmet>
      <title>{t('material_assignment_edit_page.head_title')}</title>
    </Helmet>
  );
}
