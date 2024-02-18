import { Space } from 'antd';

import ColorModeButton from './color-mode.button';
import HeaderAvatar from './header.avatar';
import LanguageButton from './language.button';

export default function HeaderRightItems() {
  return (
    <Space size="middle" align="center">
      <LanguageButton />
      <ColorModeButton />
      <HeaderAvatar />
    </Space>
  );
}
