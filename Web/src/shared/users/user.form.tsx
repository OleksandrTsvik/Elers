import { Button, Checkbox, Form, Input } from 'antd';
import { useTranslation } from 'react-i18next';

import useUserRules from './use-user.rules';
import { ErrorAlert } from '../../components';
import { UserRole } from '../../models/role.interface';

export interface UserFormValues {
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  patronymic: string;
  roleIds: string[];
}

interface Props {
  initialValues: UserFormValues;
  roles?: UserRole[];
  textOnSubmitButton: string;
  isLoading: boolean;
  isError: boolean;
  error: unknown;
  onSubmit: (values: UserFormValues) => Promise<void> | void;
}

export default function UserForm({
  initialValues,
  roles,
  textOnSubmitButton,
  isLoading,
  isError,
  error,
  onSubmit,
}: Props) {
  const { t } = useTranslation();

  const [form] = Form.useForm<UserFormValues>();
  const rules = useUserRules();

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
        name="email"
        label={t('users_page.email')}
        rules={rules.email}
      >
        <Input />
      </Form.Item>

      <Form.Item
        hasFeedback
        name="password"
        label={t('users_page.password')}
        rules={rules.password}
      >
        <Input />
      </Form.Item>

      <Form.Item
        hasFeedback
        name="firstName"
        label={t('users_page.firstName')}
        rules={rules.firstName}
      >
        <Input />
      </Form.Item>

      <Form.Item
        hasFeedback
        name="lastName"
        label={t('users_page.lastName')}
        rules={rules.lastName}
      >
        <Input />
      </Form.Item>

      <Form.Item
        hasFeedback
        name="patronymic"
        label={t('users_page.patronymic')}
        rules={rules.patronymic}
      >
        <Input />
      </Form.Item>

      {roles && (
        <Form.Item hasFeedback name="roleIds" label={t('users_page.roles')}>
          <Checkbox.Group style={{ flexDirection: 'column' }}>
            {roles.map((role) => (
              <Checkbox key={role.id} value={role.id}>
                {role.name}
              </Checkbox>
            ))}
          </Checkbox.Group>
        </Form.Item>
      )}

      <Form.Item>
        <Button block type="primary" htmlType="submit" loading={isLoading}>
          {textOnSubmitButton}
        </Button>
      </Form.Item>
    </Form>
  );
}