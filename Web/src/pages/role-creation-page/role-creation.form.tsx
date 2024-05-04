import { Skeleton } from 'antd';
import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';

import { useGetListPermissionsQuery } from '../../api/permissions.api';
import { useCreateRoleMutation } from '../../api/roles.api';
import { RoleForm, RoleFormValues } from '../../shared';

export default function RoleCreationForm() {
  const { t } = useTranslation();
  const navigate = useNavigate();

  const { data, isFetching } = useGetListPermissionsQuery();
  const [createRole, { isLoading, error }] = useCreateRoleMutation();

  const handleSubmit = async (values: RoleFormValues) => {
    await createRole(values)
      .unwrap()
      .then(() => navigate('/roles'));
  };

  if (isFetching) {
    return <Skeleton active />;
  }

  return (
    <RoleForm
      initialValues={{ name: '', permissionIds: [] }}
      permissions={data}
      textOnSubmitButton={t('actions.add')}
      isLoading={isLoading}
      error={error}
      onSubmit={handleSubmit}
    />
  );
}
