import { Button, Flex, Typography } from 'antd';
import { useTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';

import RolesHead from './roles.head';
import RolesTable from './roles.table';

export default function RolesPage() {
  const { t } = useTranslation();

  return (
    <>
      <RolesHead />
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
        <Link to="/roles/add">
          <Button type="primary">{t('roles_page.create_role')}</Button>
        </Link>
      </Flex>
      <RolesTable />
    </>
  );
}
