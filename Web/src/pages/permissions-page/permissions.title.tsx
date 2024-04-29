import { Typography } from 'antd';
import { useTranslation } from 'react-i18next';

export default function PermissionsTitle() {
  const { t } = useTranslation();

  return (
    <Typography.Title style={{ marginTop: 0 }}>
      {t('permissions_page.title')}
    </Typography.Title>
  );
}
