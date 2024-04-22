import { Typography } from 'antd';
import { useTranslation } from 'react-i18next';

import PermissionsHead from './permissions.head';
import PermissionsTable from './permissions.table';

export default function PermissionsPage() {
  const { t } = useTranslation();

  return (
    <>
      <PermissionsHead />
      <Typography.Title style={{ marginTop: 0 }}>
        {t('permissions_page.title')}
      </Typography.Title>
      <PermissionsTable />
    </>
  );
}
