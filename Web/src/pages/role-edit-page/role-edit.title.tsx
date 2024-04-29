import { Typography } from 'antd';
import { useTranslation } from 'react-i18next';

export default function RoleEditTitle() {
  const { t } = useTranslation();

  return (
    <Typography.Title style={{ marginTop: 0, textAlign: 'center' }}>
      {t('role_edit_page.title')}
    </Typography.Title>
  );
}
