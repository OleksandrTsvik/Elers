import { Typography } from 'antd';
import { useTranslation } from 'react-i18next';

export default function LoginTitle() {
  const { t } = useTranslation();

  return (
    <Typography.Title style={{ textAlign: 'center' }}>
      {t('login_page.title')}
    </Typography.Title>
  );
}
