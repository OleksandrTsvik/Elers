import { Typography } from 'antd';
import { useTranslation } from 'react-i18next';

export default function RoleCreationTitle() {
  const { t } = useTranslation();

  return (
    <Typography.Title style={{ marginTop: 0, textAlign: 'center' }}>
      {t('role_creation_page.title')}
    </Typography.Title>
  );
}
