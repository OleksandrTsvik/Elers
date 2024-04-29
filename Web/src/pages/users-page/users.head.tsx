import { Helmet } from 'react-helmet-async';
import { useTranslation } from 'react-i18next';

export default function UsersHead() {
  const { t } = useTranslation();

  return (
    <Helmet>
      <title>{t('users_page.head_title')}</title>
    </Helmet>
  );
}
