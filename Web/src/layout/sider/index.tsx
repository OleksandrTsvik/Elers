import { Drawer, Layout, theme } from 'antd';

import SiderMenu from './sider.menu';
import useBreakpointValue from '../../hooks/use-breakpoint-value';
import {
  HEADER_HEIGHT,
  SIDER_COLLAPSED_WIDTH,
  SIDER_WIDTH,
} from '../layout.constants';

interface Props {
  collapsed: boolean;
  setCollapsed: (value: boolean) => void;
}

export default function Sider({ collapsed, setCollapsed }: Props) {
  const {
    token: { colorBgContainer },
  } = theme.useToken();

  return (
    <>
      {useBreakpointValue({
        lg: (
          <Layout.Sider
            collapsible
            breakpoint="md"
            trigger={null}
            collapsed={collapsed}
            width={SIDER_WIDTH}
            collapsedWidth={SIDER_COLLAPSED_WIDTH}
            style={{
              position: 'sticky',
              top: HEADER_HEIGHT,
              height: `calc(100vh - ${HEADER_HEIGHT}px)`,
              overflow: 'auto',
              background: colorBgContainer,
              borderRight: '1px solid rgba(5, 5, 5, 0.06)',
            }}
          >
            <SiderMenu />
          </Layout.Sider>
        ),
        xs: (
          <Drawer
            width="100%"
            placement="left"
            open={!collapsed}
            styles={{ body: { padding: 0 } }}
            onClose={() => setCollapsed(true)}
          >
            <SiderMenu />
          </Drawer>
        ),
      })}
    </>
  );
}
