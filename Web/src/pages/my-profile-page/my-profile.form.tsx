import { SaveOutlined } from '@ant-design/icons';
import { Button, DatePicker, Flex, Form, Input } from 'antd';
import dayjs from 'dayjs';
import { useTranslation } from 'react-i18next';

import useMyProfileRules from './use-my-profile.rules';
import { useUpdateCurrentProfileMutation } from '../../api/profile.api';
import { ErrorForm } from '../../common/error';
import { USER_RULES } from '../../common/rules';
import { DATE_FORMAT } from '../../utils/constants/app.constants';

import styles from './my-profile.module.scss';

interface FormValues {
  email: string;
  firstName: string;
  lastName: string;
  patronymic: string;
  birthDate?: Date;
}

interface Props {
  initialValues: FormValues;
  onFinish?: () => void;
}

export default function MyProfileForm({ initialValues, onFinish }: Props) {
  const { t } = useTranslation();

  const [form] = Form.useForm<FormValues>();
  const rules = useMyProfileRules();

  const [updateCurrentProfile, { isLoading, error }] =
    useUpdateCurrentProfileMutation();

  const handleFinish = async (values: FormValues) => {
    await updateCurrentProfile(values)
      .unwrap()
      .then(() => onFinish && onFinish());
  };

  return (
    <Form
      className={styles.form}
      form={form}
      layout="vertical"
      initialValues={{
        ...initialValues,
        birthDate: initialValues.birthDate
          ? dayjs(initialValues.birthDate)
          : undefined,
      }}
      onFinish={handleFinish}
    >
      <ErrorForm error={error} form={form} />

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
        name="firstName"
        label={t('users_page.firstName')}
        rules={rules.firstName}
      >
        <Input showCount maxLength={USER_RULES.firstName.max} />
      </Form.Item>

      <Form.Item
        hasFeedback
        name="patronymic"
        label={t('users_page.patronymic')}
        rules={rules.patronymic}
      >
        <Input showCount maxLength={USER_RULES.patronymic.max} />
      </Form.Item>

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
        name="birthDate"
        label={t('my_profile_page.birth_date')}
        rules={rules.birthDate}
      >
        <DatePicker className="w-100" format={DATE_FORMAT} />
      </Form.Item>

      <Flex gap="small">
        <Button
          type="primary"
          htmlType="submit"
          icon={<SaveOutlined />}
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
