import { Flex, Layout } from 'antd';

import HeaderRightItems from './header-right-items';
import HeaderLogo from './header.logo';
import SiderCollapseButton from './sider-collapse.button';
import { AuthGuard } from '../../auth';
import useBreakpointValue from '../../hooks/use-breakpoint-value';
import useColorModeValue from '../../hooks/use-color-mode-value';
import {
  SIDER_COLLAPSED_WIDTH,
  SIDER_COLLAPSED_WIDTH_MOBILE,
} from '../layout.constants';

import styles from './header.module.scss';

interface Props {
  collapsed: boolean;
  setCollapsed: (value: boolean) => void;
}

export default function Header({ collapsed, setCollapsed }: Props) {
  return (
    <Layout.Header
      className={styles.header}
      style={{
        padding: useBreakpointValue({
          md: `0 ${SIDER_COLLAPSED_WIDTH}px`,
          xs: `0 ${SIDER_COLLAPSED_WIDTH_MOBILE}px`,
        }),
        backgroundColor: useColorModeValue('#fff', '#001529'),
      }}
    >
      <AuthGuard>
        <SiderCollapseButton
          collapsed={collapsed}
          setCollapsed={setCollapsed}
        />
      </AuthGuard>
      <Flex flex={1} justify="space-between" align="center">
        <HeaderLogo />
        <HeaderRightItems />
      </Flex>
    </Layout.Header>
  );
}
