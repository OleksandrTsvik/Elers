import { Helmet } from 'react-helmet-async';
import { useTranslation } from 'react-i18next';

export default function MaterialAssignmentCreationHead() {
  const { t } = useTranslation();

  return (
    <Helmet>
      <title>{t('material_assignment_creation_page.head_title')}</title>
    </Helmet>
  );
}
