import { Flex, Typography } from 'antd';
import { useTranslation } from 'react-i18next';

import LoginForm from './login.form';

export default function LoginPage() {
  const { t } = useTranslation();

  return (
    <Flex justify="center" align="center">
      <Flex vertical style={{ width: 320 }}>
        <Typography.Title style={{ textAlign: 'center' }}>
          {t('login_page.title')}
        </Typography.Title>
        <LoginForm />
      </Flex>
    </Flex>
  );
}
