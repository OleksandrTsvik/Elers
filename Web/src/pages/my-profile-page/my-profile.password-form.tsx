import { Button, Flex, Form, Input } from 'antd';
import { useTranslation } from 'react-i18next';

import usePasswordRules from './use-password.rules';
import { useChangeCurrentUserPasswordMutation } from '../../api/profile.api';
import { ErrorForm } from '../../common/error';
import { USER_RULES } from '../../common/rules';

import styles from './my-profile.module.scss';

interface FormValues {
  currentPassword: string;
  newPassword: string;
  confirmPassword: string;
}

interface Props {
  onFinish?: () => void;
}

export default function MyProfilePasswordForm({ onFinish }: Props) {
  const { t } = useTranslation();

  const [form] = Form.useForm<FormValues>();
  const rules = usePasswordRules();

  const [changePassword, { isLoading, error }] =
    useChangeCurrentUserPasswordMutation();

  const handleFinish = async (values: FormValues) => {
    await changePassword(values)
      .unwrap()
      .then(() => onFinish && onFinish());
  };

  return (
    <Form
      className={styles.form}
      form={form}
      layout="vertical"
      onFinish={handleFinish}
    >
      <ErrorForm error={error} form={form} />

      <Form.Item
        hasFeedback
        name="currentPassword"
        label={t('users_page.currentPassword')}
        rules={rules.currentPassword}
      >
        <Input.Password showCount maxLength={USER_RULES.password.max} />
      </Form.Item>

      <Form.Item
        hasFeedback
        name="newPassword"
        label={t('users_page.newPassword')}
        rules={rules.newPassword}
      >
        <Input.Password showCount maxLength={USER_RULES.password.max} />
      </Form.Item>

      <Form.Item
        hasFeedback
        name="confirmPassword"
        label={t('users_page.confirmPassword')}
        rules={rules.confirmPassword}
      >
        <Input.Password showCount maxLength={USER_RULES.password.max} />
      </Form.Item>

      <Flex gap="small">
        <Button
          type="primary"
          htmlType="submit"
          loading={isLoading}
          style={{ flex: 1 }}
        >
          {t('actions.save_changes')}
        </Button>
        <Button onClick={onFinish}>{t('actions.cancel')}</Button>
      </Flex>
    </Form>
  );
}
