import { Helmet } from 'react-helmet-async';
import { useTranslation } from 'react-i18next';

export default function RoleEditHead() {
  const { t } = useTranslation();

  return (
    <Helmet>
      <title>{t('role_edit_page.head_title')}</title>
    </Helmet>
  );
}
