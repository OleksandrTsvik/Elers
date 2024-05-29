import { Button, Flex, Typography } from 'antd';
import { useTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';

import { PermissionType, PermissionsGuard } from '../../auth';

export default function RolesTitle() {
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
        {t('roles_page.title')}
      </Typography.Title>
      <PermissionsGuard permissions={PermissionType.CreateRole}>
        <Link to="/roles/add">
          <Button type="primary">{t('roles_page.create_role')}</Button>
        </Link>
      </PermissionsGuard>
    </Flex>
  );
}
