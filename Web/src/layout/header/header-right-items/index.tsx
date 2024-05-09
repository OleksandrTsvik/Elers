import { Space } from 'antd';

import ColorModeButton from './color-mode.button';
import HeaderAvatar from './header.avatar';
import LanguageButton from './language.button';
import LoginButton from './login.button';
import useAuth from '../../../auth/use-auth';

export default function HeaderRightItems() {
  const { isAuth } = useAuth();

  return (
    <Space size="middle" align="center">
      <LanguageButton />
      <ColorModeButton />
      {isAuth ? <HeaderAvatar /> : <LoginButton />}
    </Space>
  );
}
