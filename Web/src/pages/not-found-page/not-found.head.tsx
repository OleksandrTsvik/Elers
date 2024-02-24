import { Helmet } from 'react-helmet-async';
import { useTranslation } from 'react-i18next';

export default function NotFoundHead() {
  const { t } = useTranslation();

  return (
    <Helmet>
      <title>{t('not_found_page.head_title')}</title>
    </Helmet>
  );
}
