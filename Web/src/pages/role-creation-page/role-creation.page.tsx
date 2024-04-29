import RoleCreationBreadcrumb from './role-creation.breadcrumb';
import RoleCreationForm from './role-creation.form';
import RoleCreationHead from './role-creation.head';
import RoleCreationTitle from './role-creation.title';

export default function RoleCreationPage() {
  return (
    <>
      <RoleCreationHead />
      <RoleCreationBreadcrumb />
      <RoleCreationTitle />
      <RoleCreationForm />
    </>
  );
}
