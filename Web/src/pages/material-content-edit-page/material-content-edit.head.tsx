import { Helmet } from 'react-helmet-async';
import { useTranslation } from 'react-i18next';

export default function MaterialContentEditHead() {
  const { t } = useTranslation();

  return (
    <Helmet>
      <title>{t('material_content_edit_page.head_title')}</title>
    </Helmet>
  );
}
