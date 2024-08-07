import { Skeleton } from 'antd';
import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';

import { getInitialValues } from './role-edit-page.utils';
import {
  useGetRoleByIdQuery,
  useUpdateRoleMutation,
} from '../../api/roles.api';
import { NavigateToNotFound } from '../../common/navigate';
import { RoleForm, RoleFormValues } from '../../shared';

interface Props {
  roleId: string;
}

export default function RoleEditForm({ roleId }: Props) {
  const { t } = useTranslation();
  const navigate = useNavigate();

  const { data, isFetching } = useGetRoleByIdQuery({ id: roleId });
  const [updateRole, { isLoading, error }] = useUpdateRoleMutation();

  const handleSubmit = async (values: RoleFormValues) => {
    await updateRole({ roleId, ...values })
      .unwrap()
      .then(() => navigate('/roles'));
  };

  if (isFetching) {
    return <Skeleton active />;
  }

  if (!data) {
    return <NavigateToNotFound />;
  }

  return (
    <RoleForm
      initialValues={getInitialValues(data)}
      permissions={data.permissions}
      textOnSubmitButton={t('actions.save_changes')}
      isLoading={isLoading}
      error={error}
      onSubmit={handleSubmit}
    />
  );
}
