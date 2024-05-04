import { Button, Checkbox, Form, Input } from 'antd';
import { useTranslation } from 'react-i18next';

import useUserRules from './use-user.rules';
import { FormMode } from '../../models/form-mode.enum';
import { UserRole } from '../../models/role.interface';
import { ErrorForm } from '../error';
import { USER_RULES } from '../rules';

export interface UserFormValues {
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  patronymic: string;
  roleIds: string[];
}

interface Props {
  mode: FormMode;
  initialValues: UserFormValues;
  roles?: UserRole[];
  textOnSubmitButton: string;
  isLoading: boolean;
  error: unknown;
  onSubmit: (values: UserFormValues) => Promise<void> | void;
}

export default function UserForm({
  mode,
  initialValues,
  roles,
  textOnSubmitButton,
  isLoading,
  error,
  onSubmit,
}: Props) {
  const { t } = useTranslation();

  const [form] = Form.useForm<UserFormValues>();
  const rules = useUserRules(mode);

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
        name="email"
        label={t('users_page.email')}
        rules={rules.email}
      >
        <Input showCount maxLength={USER_RULES.email.max} />
      </Form.Item>

      <Form.Item
        hasFeedback
        name="password"
        label={t('users_page.password')}
        rules={rules.password}
      >
        <Input showCount maxLength={USER_RULES.password.max} />
      </Form.Item>

      <Form.Item
        hasFeedback
        name="firstName"
        label={t('users_page.firstName')}
        rules={rules.firstName}
      >
        <Input showCount maxLength={USER_RULES.firstName.max} />
      </Form.Item>

      <Form.Item
        hasFeedback
        name="lastName"
        label={t('users_page.lastName')}
        rules={rules.lastName}
      >
        <Input showCount maxLength={USER_RULES.lastName.max} />
      </Form.Item>

      <Form.Item
        hasFeedback
        name="patronymic"
        label={t('users_page.patronymic')}
        rules={rules.patronymic}
      >
        <Input showCount maxLength={USER_RULES.patronymic.max} />
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
