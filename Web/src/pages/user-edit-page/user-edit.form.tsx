import { Skeleton } from 'antd';
import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';

import { getInitialValues } from './user-edit-page.utils';
import { useGetListUserRolesQuery } from '../../api/roles.api';
import {
  useGetUserByIdQuery,
  useUpdateUserMutation,
} from '../../api/users.api';
import { FormMode } from '../../models/form-mode.enum';
import { NavigateToNotFound, UserForm, UserFormValues } from '../../shared';

interface Props {
  userId: string;
}

export default function UserEditForm({ userId }: Props) {
  const { t } = useTranslation();
  const navigate = useNavigate();

  const { data, isFetching } = useGetUserByIdQuery({ id: userId });

  const { data: dataRoles, isFetching: isFetchingRoles } =
    useGetListUserRolesQuery();

  const [updateUser, { isLoading, isError, error }] = useUpdateUserMutation();

  const handleSubmit = async (values: UserFormValues) => {
    await updateUser({ userId, ...values })
      .unwrap()
      .then(() => navigate('/users'));
  };

  if (isFetching || isFetchingRoles) {
    return <Skeleton active />;
  }

  if (!data) {
    return <NavigateToNotFound />;
  }

  return (
    <UserForm
      mode={FormMode.Edit}
      initialValues={getInitialValues(data, dataRoles)}
      roles={dataRoles}
      textOnSubmitButton={t('actions.save_changes')}
      isLoading={isLoading}
      isError={isError}
      error={error}
      onSubmit={handleSubmit}
    />
  );
}
