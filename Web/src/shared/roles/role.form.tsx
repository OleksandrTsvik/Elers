import { Button, Form, Input } from 'antd';
import { Key } from 'antd/es/table/interface';
import { useTranslation } from 'react-i18next';

import PermissionsTable from './permissions.table';
import useRoleRules from './use-role.rules';
import { Permission } from '../../models/permission.interface';
import { ErrorForm } from '../error';
import { ROLE_RULES } from '../rules';

export interface RoleFormValues {
  name: string;
  permissionIds: string[];
}

interface Props {
  initialValues: RoleFormValues;
  permissions?: Permission[];
  textOnSubmitButton: string;
  isLoading: boolean;
  error: unknown;
  onSubmit: (values: RoleFormValues) => Promise<void> | void;
}

export default function RoleForm({
  initialValues,
  permissions,
  textOnSubmitButton,
  isLoading,
  error,
  onSubmit,
}: Props) {
  const { t } = useTranslation();

  const [form] = Form.useForm<RoleFormValues>();
  const rules = useRoleRules();

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
      <ErrorForm error={error} />

      <Form.Item
        hasFeedback
        name="name"
        label={t('roles_page.name')}
        rules={rules.name}
      >
        <Input showCount maxLength={ROLE_RULES.name.max} />
      </Form.Item>

      <Form.Item name="permissionIds" label={t('roles_page.permissionIds')}>
        <PermissionsTable
          permissions={permissions}
          defaultSelectedRowKeys={initialValues.permissionIds}
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
