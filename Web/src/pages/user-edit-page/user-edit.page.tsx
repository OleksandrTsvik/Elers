import { useParams } from 'react-router-dom';

import UserEditBreadcrumb from './user-edit.breadcrumb';
import UserEditForm from './user-edit.form';
import UserEditHead from './user-edit.head';
import UserEditTitle from './user-edit.title';
import { NavigateToNotFound } from '../../common/navigate';

export default function UserEditPage() {
  const { userId } = useParams();

  if (!userId) {
    return <NavigateToNotFound />;
  }

  return (
    <>
      <UserEditHead />
      <UserEditBreadcrumb />
      <UserEditTitle />
      <UserEditForm userId={userId} />
    </>
  );
}
