import { Typography } from 'antd';
import { useTranslation } from 'react-i18next';

export default function UserEditTitle() {
  const { t } = useTranslation();

  return (
    <Typography.Title style={{ marginTop: 0, textAlign: 'center' }}>
      {t('user_edit_page.title')}
    </Typography.Title>
  );
}
