import { Grid, Layout, theme } from 'antd';
import { Outlet } from 'react-router-dom';

import { CONTENT_MAX_WIDTH } from '../layout.constants';

export default function Main() {
  const breakpoints = Grid.useBreakpoint();

  const {
    token: { colorBgContainer, borderRadiusLG },
  } = theme.useToken();

  return (
    <Layout.Content
      style={{
        padding: breakpoints.lg ? 24 : 12,
        marginTop: breakpoints.md ? 16 : undefined,
        background: colorBgContainer,
        borderRadius: breakpoints.md ? borderRadiusLG : undefined,
      }}
    >
      <div style={{ margin: '0 auto', maxWidth: CONTENT_MAX_WIDTH }}>
        <Outlet />
      </div>
    </Layout.Content>
  );
}
