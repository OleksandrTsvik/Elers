import { Helmet } from 'react-helmet-async';
import { useTranslation } from 'react-i18next';

export default function LoginHead() {
  const { t } = useTranslation();

  return (
    <Helmet>
      <title>{t('login_page.title')}</title>
    </Helmet>
  );
}
