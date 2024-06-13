import { Helmet } from 'react-helmet-async';
import { useTranslation } from 'react-i18next';

export default function MyProgressHead() {
  const { t } = useTranslation();

  return (
    <Helmet>
      <title>{t('my_progress_page.head_title')}</title>
    </Helmet>
  );
}
