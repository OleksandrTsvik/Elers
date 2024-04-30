import { Helmet } from 'react-helmet-async';
import { useTranslation } from 'react-i18next';

export default function UserCreationHead() {
  const { t } = useTranslation();

  return (
    <Helmet>
      <title>{t('user_creation_page.head_title')}</title>
    </Helmet>
  );
}
