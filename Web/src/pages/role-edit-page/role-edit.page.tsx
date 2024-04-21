import { Typography } from 'antd';
import { useTranslation } from 'react-i18next';
import { useParams } from 'react-router-dom';

import RoleEditBreadcrumb from './role-edit.breadcrumb';
import RoleEditForm from './role-edit.form';
import RoleEditHead from './role-edit.head';
import { NavigateToNotFound } from '../../components';

export default function RoleEditPage() {
  const { roleId } = useParams();
  const { t } = useTranslation();

  if (!roleId) {
    return <NavigateToNotFound />;
  }

  return (
    <>
      <RoleEditHead />
      <RoleEditBreadcrumb />
      <Typography.Title style={{ marginTop: 0, textAlign: 'center' }}>
        {t('role_edit_page.title')}
      </Typography.Title>
      <RoleEditForm roleId={roleId} />
    </>
  );
}
