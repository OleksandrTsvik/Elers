import { Helmet } from 'react-helmet-async';
import { useTranslation } from 'react-i18next';

export default function MaterialFileCreationHead() {
  const { t } = useTranslation();

  return (
    <Helmet>
      <title>{t('material_file_creation_page.head_title')}</title>
    </Helmet>
  );
}
