import { useParams } from 'react-router-dom';

import RoleEditBreadcrumb from './role-edit.breadcrumb';
import RoleEditForm from './role-edit.form';
import RoleEditHead from './role-edit.head';
import RoleEditTitle from './role-edit.title';
import { NavigateToNotFound } from '../../components';

export default function RoleEditPage() {
  const { roleId } = useParams();

  if (!roleId) {
    return <NavigateToNotFound />;
  }

  return (
    <>
      <RoleEditHead />
      <RoleEditBreadcrumb />
      <RoleEditTitle />
      <RoleEditForm roleId={roleId} />
    </>
  );
}
