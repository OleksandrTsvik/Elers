import { Skeleton } from 'antd';
import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';

import { useGetListUserRolesQuery } from '../../api/roles.api';
import { useCreateUserMutation } from '../../api/users.api';
import { FormMode } from '../../common/types';
import { UserForm, UserFormValues } from '../../shared';

export default function UserCreationForm() {
  const { t } = useTranslation();
  const navigate = useNavigate();

  const { data, isFetching } = useGetListUserRolesQuery();
  const [createUser, { isLoading, error }] = useCreateUserMutation();

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
      mode={FormMode.Creation}
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
      error={error}
      onSubmit={handleSubmit}
    />
  );
}
