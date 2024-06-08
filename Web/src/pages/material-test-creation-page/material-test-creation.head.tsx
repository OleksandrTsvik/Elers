import { Helmet } from 'react-helmet-async';
import { useTranslation } from 'react-i18next';

export default function MaterialTestCreationHead() {
  const { t } = useTranslation();

  return (
    <Helmet>
      <title>{t('material_test_creation_page.head_title')}</title>
    </Helmet>
  );
}
