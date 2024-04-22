import { Typography } from 'antd';
import { useTranslation } from 'react-i18next';

import RoleCreationBreadcrumb from './role-creation.breadcrumb';
import RoleCreationForm from './role-creation.form';
import RoleCreationHead from './role-creation.head';

export default function RoleCreationPage() {
  const { t } = useTranslation();

  return (
    <>
      <RoleCreationHead />
      <RoleCreationBreadcrumb />
      <Typography.Title style={{ marginTop: 0, textAlign: 'center' }}>
        {t('role_creation_page.title')}
      </Typography.Title>
      <RoleCreationForm />
    </>
  );
}
