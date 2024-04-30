import UserCreationBreadcrumb from './user-creation.breadcrumb';
import UserCreationForm from './user-creation.form';
import UserCreationHead from './user-creation.head';
import UserCreationTitle from './user-creation.title';

export default function UserCreationPage() {
  return (
    <>
      <UserCreationHead />
      <UserCreationBreadcrumb />
      <UserCreationTitle />
      <UserCreationForm />
    </>
  );
}
