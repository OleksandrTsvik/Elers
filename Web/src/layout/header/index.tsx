import { Flex, Layout } from 'antd';

import HeaderRightItems from './header-right-items';
import HeaderLogo from './header.logo';
import SiderCollapseButton from './sider-collapse.button';
import useAuth from '../../hooks/use-auth';
import useBreakpointValue from '../../hooks/use-breakpoint-value';
import useColorModeValue from '../../hooks/use-color-mode-value';
import {
  SIDER_COLLAPSED_WIDTH,
  SIDER_COLLAPSED_WIDTH_MOBILE,
} from '../layout.constants';

interface Props {
  collapsed: boolean;
  setCollapsed: (value: boolean) => void;
}

export default function Header({ collapsed, setCollapsed }: Props) {
  const { isAuth } = useAuth();

  return (
    <Layout.Header
      style={{
        position: 'sticky',
        top: 0,
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
        width: '100%',
        padding: useBreakpointValue({
          md: `0 ${SIDER_COLLAPSED_WIDTH}px`,
          xs: `0 ${SIDER_COLLAPSED_WIDTH_MOBILE}px`,
        }),
        backgroundColor: useColorModeValue('#fff', '#001529'),
        borderBlockEnd: '1px solid rgba(5, 5, 5, 0.06)',
        zIndex: 1,
      }}
    >
      {isAuth && (
        <SiderCollapseButton
          collapsed={collapsed}
          setCollapsed={setCollapsed}
        />
      )}
      <Flex flex={1} justify="space-between" align="center">
        <HeaderLogo />
        <HeaderRightItems />
      </Flex>
    </Layout.Header>
  );
}
