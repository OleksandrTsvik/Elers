import { MoonOutlined, SunOutlined } from '@ant-design/icons';
import { Button } from 'antd';

import useColorMode from '../../../hooks/use-color-mode';
import useColorModeValue from '../../../hooks/use-color-mode-value';

export default function ColorModeButton() {
  const { toggleColorMode } = useColorMode();

  return (
    <Button
      type="text"
      icon={useColorModeValue(<SunOutlined />, <MoonOutlined />)}
      onClick={toggleColorMode}
    />
  );
}
