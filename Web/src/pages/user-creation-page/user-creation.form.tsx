import { Skeleton } from 'antd';
import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';

import { useGetListUserRolesQuery } from '../../api/roles.api';
import { useCreateUserMutation } from '../../api/users.api';
import { UserForm, UserFormValues } from '../../shared';

export default function UserCreationForm() {
  const { t } = useTranslation();
  const navigate = useNavigate();

  const { data, isFetching } = useGetListUserRolesQuery();
  const [createUser, { isLoading, isError, error }] = useCreateUserMutation();

  const handleSubmit = async (values: UserFormValues) => {
    await createUser(values)
      .unwrap()
      .then(() => navigate('/users'));
  };

  if (isFetching) {
    return <Skeleton active />;
  }

  return (
    <UserForm
      initialValues={{
        email: '',
        password: '',
        firstName: '',
        lastName: '',
        patronymic: '',
        roleIds: [],
      }}
      roles={data}
      textOnSubmitButton={t('actions.add')}
      isLoading={isLoading}
      isError={isError}
      error={error}
      onSubmit={handleSubmit}
    />
  );
}
