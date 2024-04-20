import { Helmet } from 'react-helmet-async';
import { useTranslation } from 'react-i18next';

export default function RolesHead() {
  const { t } = useTranslation();

  return (
    <Helmet>
      <title>{t('roles_page.head_title')}</title>
    </Helmet>
  );
}
