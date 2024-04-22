import { Helmet } from 'react-helmet-async';
import { useTranslation } from 'react-i18next';

export default function RoleCreationHead() {
  const { t } = useTranslation();

  return (
    <Helmet>
      <title>{t('role_creation_page.head_title')}</title>
    </Helmet>
  );
}
