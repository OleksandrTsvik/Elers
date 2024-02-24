import { MenuFoldOutlined, MenuUnfoldOutlined } from '@ant-design/icons';
import { Button } from 'antd';

import useBreakpointValue from '../../hooks/use-breakpoint-value';
import useColorModeValue from '../../hooks/use-color-mode-value';
import {
  HEADER_HEIGHT,
  SIDER_COLLAPSED_WIDTH,
  SIDER_COLLAPSED_WIDTH_MOBILE,
} from '../layout.constants';

import styles from './header.module.scss';

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
      className={styles.siderCollapse}
      type="link"
      icon={collapsed ? <MenuUnfoldOutlined /> : <MenuFoldOutlined />}
      style={{
        width: useBreakpointValue({
          md: SIDER_COLLAPSED_WIDTH,
          xs: SIDER_COLLAPSED_WIDTH_MOBILE,
        }),
        height: HEADER_HEIGHT,
        color: useColorModeValue('#8c8c8c', 'rgba(255, 255, 255, 0.85)'),
      }}
      onClick={() => setCollapsed(!collapsed)}
    />
  );
}
