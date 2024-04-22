import { Button, Form, Input } from 'antd';
import { Key } from 'antd/es/table/interface';
import { useTranslation } from 'react-i18next';

import PermissionsTable from './permissions.table';
import useRoleEditRules from './use-role.rules';
import { ErrorAlert } from '../../components';
import { Permission } from '../../models/permission.interface';

export interface RoleFormValues {
  name: string;
  permissionIds: string[];
}

interface Props {
  initialValues?: RoleFormValues;
  permissions: Permission[];
  textOnSubmitButton: string;
  isLoading: boolean;
  isError: boolean;
  error: unknown;
  onSubmit: (values: RoleFormValues) => Promise<void> | void;
}

export default function RoleForm({
  initialValues,
  permissions,
  textOnSubmitButton,
  isLoading,
  isError,
  error,
  onSubmit,
}: Props) {
  const { t } = useTranslation();

  const [form] = Form.useForm<RoleFormValues>();
  const rules = useRoleEditRules();

  const handleChangePermissionIds = (permissionIds: Key[]) => {
    form.setFieldValue('permissionIds', permissionIds);
  };

  return (
    <Form
      form={form}
      layout="vertical"
      initialValues={initialValues}
      onFinish={onSubmit}
    >
      {isError && (
        <Form.Item>
          <ErrorAlert error={error} />
        </Form.Item>
      )}

      <Form.Item
        hasFeedback
        name="name"
        label={t('roles_page.name')}
        rules={rules.name}
      >
        <Input />
      </Form.Item>

      <Form.Item name="permissionIds" label={t('roles_page.permissionIds')}>
        <PermissionsTable
          permissions={permissions}
          defaultSelectedRowKeys={initialValues?.permissionIds}
          onChangeRowSelection={handleChangePermissionIds}
        />
      </Form.Item>

      <Form.Item>
        <Button block type="primary" htmlType="submit" loading={isLoading}>
          {textOnSubmitButton}
        </Button>
      </Form.Item>
    </Form>
  );
}
