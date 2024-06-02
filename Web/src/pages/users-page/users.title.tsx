import { Button, Flex, Typography } from 'antd';
import { useTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';

import { PermissionType, PermissionsGuard } from '../../auth';

export default function UsersTitle() {
  const { t } = useTranslation();

  return (
    <Flex
      justify="space-between"
      align="center"
      gap="large"
      wrap="wrap"
      style={{ marginBottom: 18 }}
    >
      <Typography.Title style={{ margin: 0 }}>
        {t('users_page.title')}
      </Typography.Title>
      <PermissionsGuard permissions={PermissionType.CreateUser}>
        <Link to="/users/add">
          <Button type="primary">{t('users_page.create_user')}</Button>
        </Link>
      </PermissionsGuard>
    </Flex>
  );
}
