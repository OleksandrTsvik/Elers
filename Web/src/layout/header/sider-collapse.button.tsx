import { MenuFoldOutlined, MenuUnfoldOutlined } from '@ant-design/icons';
import { Button } from 'antd';

import useBreakpointValue from '../../hooks/use-breakpoint-value';
import useColorModeValue from '../../hooks/use-color-mode-value';
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
  return (
    <Button
      type="link"
      style={{
        position: 'absolute',
        top: 0,
        left: 0,
        fontSize: '16px',
        width: useBreakpointValue({
          md: SIDER_COLLAPSED_WIDTH,
          xs: SIDER_COLLAPSED_WIDTH_MOBILE,
        }),
        height: HEADER_HEIGHT,
        color: useColorModeValue('#8c8c8c', 'rgba(255, 255, 255, 0.85)'),
      }}
      icon={collapsed ? <MenuUnfoldOutlined /> : <MenuFoldOutlined />}
      onClick={() => setCollapsed(!collapsed)}
    />
  );
}
