import { Button, Form, Input, Skeleton } from 'antd';
import { Key } from 'antd/es/table/interface';
import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';

import PermissionsTable from './permissions.table';
import { getInitialValues } from './role-edit-page.utils';
import useRoleEditRules from './use-role-edit.rules';
import {
  useGetRoleByIdQuery,
  useUpdateRoleMutation,
} from '../../api/roles.api';
import { ErrorAlert, NavigateToNotFound } from '../../components';

export interface FormValues {
  name: string;
  permissionIds: string[];
}

interface Props {
  roleId: string;
}

export default function RoleEditForm({ roleId }: Props) {
  const { t } = useTranslation();
  const navigate = useNavigate();

  const { data, isFetching } = useGetRoleByIdQuery({ id: roleId });
  const [form] = Form.useForm<FormValues>();
  const rules = useRoleEditRules();

  const [updateRole, { isLoading, isError, error }] = useUpdateRoleMutation();

  const handleChangePermissionIds = (permissionIds: Key[]) => {
    form.setFieldValue('permissionIds', permissionIds);
  };

  const handleSubmit = async (values: FormValues) => {
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
    <Form
      form={form}
      layout="vertical"
      initialValues={getInitialValues(data)}
      onFinish={handleSubmit}
    >
      {isError && (
        <Form.Item>
          <ErrorAlert error={error} />
        </Form.Item>
      )}

      <Form.Item
        hasFeedback
        name="name"
        label={t('role_edit_page.name')}
        rules={rules.name}
      >
        <Input />
      </Form.Item>

      <Form.Item name="permissionIds" label={t('role_edit_page.permissionIds')}>
        <PermissionsTable
          data={data.permissions}
          onChangeRowSelection={handleChangePermissionIds}
        />
      </Form.Item>

      <Form.Item>
        <Button block type="primary" htmlType="submit" loading={isLoading}>
          {t('role_edit_page.submit')}
        </Button>
      </Form.Item>
    </Form>
  );
}
