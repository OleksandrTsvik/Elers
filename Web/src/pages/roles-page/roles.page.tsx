import { Typography } from 'antd';
import { useTranslation } from 'react-i18next';

import RolesHead from './roles.head';
import RolesTable from './roles.table';

export default function RolesPage() {
  const { t } = useTranslation();

  return (
    <>
      <RolesHead />
      <Typography.Title style={{ marginTop: 0 }}>
        {t('roles_page.title')}
      </Typography.Title>
      <RolesTable />
    </>
  );
}
