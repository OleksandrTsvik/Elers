import { Flex } from 'antd';

import LoginForm from './login.form';
import LoginHead from './login.head';
import LoginTitle from './login.title';

export default function LoginPage() {
  return (
    <>
      <LoginHead />
      <Flex justify="center" align="center">
        <Flex vertical style={{ width: 320 }}>
          <LoginTitle />
          <LoginForm />
        </Flex>
      </Flex>
    </>
  );
}
