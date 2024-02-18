import { MenuFoldOutlined, MenuUnfoldOutlined } from '@ant-design/icons';
import { Button, Grid } from 'antd';

import { useColorModeValue } from '../../hooks/use-color-mode-value';
import {
  HEADER_HEIGHT,
  SIDER_COLLAPSED_WIDTH,
  SIDER_COLLAPSED_WIDTH_MOBILE,
} from '../layout.constants';

interface Props {
  collapsed: boolean;
  setCollapsed: (value: boolean) => void;
}

export default function SiderCollapseButton({
  collapsed,
  setCollapsed,
}: Props) {
  const breakpoints = Grid.useBreakpoint();

  return (
    <Button
      type="link"
      style={{
        position: 'absolute',
        top: 0,
        left: 0,
        fontSize: '16px',
        width: breakpoints.md
          ? SIDER_COLLAPSED_WIDTH
          : SIDER_COLLAPSED_WIDTH_MOBILE,
        height: HEADER_HEIGHT,
        color: useColorModeValue('#8c8c8c', 'rgba(255, 255, 255, 0.85)'),
      }}
      icon={collapsed ? <MenuUnfoldOutlined /> : <MenuFoldOutlined />}
      onClick={() => setCollapsed(!collapsed)}
    />
  );
}
