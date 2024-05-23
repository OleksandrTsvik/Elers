import { Helmet } from 'react-helmet-async';
import { useTranslation } from 'react-i18next';

export default function MaterialLinkEditHead() {
  const { t } = useTranslation();

  return (
    <Helmet>
      <title>{t('material_link_edit_page.head_title')}</title>
    </Helmet>
  );
}
