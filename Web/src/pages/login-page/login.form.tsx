import { LockOutlined, MailOutlined } from '@ant-design/icons';
import { Button, Form, Input } from 'antd';
import { useTranslation } from 'react-i18next';

import useLoginRules from './use-login.rules';
import useLogin from '../../auth/use-login';
import { ErrorForm } from '../../common';

interface FormValues {
  email: string;
  password: string;
}

const initialValues: FormValues = { email: '', password: '' };

export default function LoginForm() {
  const { t } = useTranslation();

  const [form] = Form.useForm<FormValues>();
  const rules = useLoginRules();

  const { login, isLoading, error } = useLogin();

  const handleSubmit = (values: FormValues) => {
    login(values);
  };

  return (
    <Form form={form} initialValues={initialValues} onFinish={handleSubmit}>
      <ErrorForm error={error} form={form} />

      <Form.Item hasFeedback name="email" rules={rules.email}>
        <Input prefix={<MailOutlined />} placeholder={t('login_page.email')} />
      </Form.Item>

      <Form.Item hasFeedback name="password" rules={rules.password}>
        <Input.Password
          prefix={<LockOutlined />}
          placeholder={t('login_page.password')}
        />
      </Form.Item>

      <Form.Item>
        <Button block type="primary" htmlType="submit" loading={isLoading}>
          {t('login_page.submit')}
        </Button>
      </Form.Item>
    </Form>
  );
}
