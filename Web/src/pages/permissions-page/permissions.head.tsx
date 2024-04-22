import { Helmet } from 'react-helmet-async';
import { useTranslation } from 'react-i18next';

export default function PermissionsHead() {
  const { t } = useTranslation();

  return (
    <Helmet>
      <title>{t('permissions_page.head_title')}</title>
    </Helmet>
  );
}
