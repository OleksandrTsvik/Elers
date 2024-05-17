import { Helmet } from 'react-helmet-async';
import { useTranslation } from 'react-i18next';

export default function MaterialContentCreationHead() {
  const { t } = useTranslation();

  return (
    <Helmet>
      <title>{t('material_text_creation_page.head_title')}</title>
    </Helmet>
  );
}
