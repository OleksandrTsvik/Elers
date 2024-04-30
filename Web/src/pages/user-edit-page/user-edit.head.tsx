import { Helmet } from 'react-helmet-async';
import { useTranslation } from 'react-i18next';

export default function UserEditHead() {
  const { t } = useTranslation();

  return (
    <Helmet>
      <title>{t('user_edit_page.head_title')}</title>
    </Helmet>
  );
}
