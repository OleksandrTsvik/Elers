import { Grid, Layout, theme } from 'antd';
import { useCallback, useLayoutEffect, useState } from 'react';
import { Outlet } from 'react-router-dom';

import Footer from './footer';
import Header from './header';
import Sider from './sider';
import useAuth from '../hooks/use-auth';
import { COLLAPSED_SIDER } from '../utils/constants/local-storage.constants';

import './layout.css';

export default function LayoutPage() {
  const breakpoints = Grid.useBreakpoint();

  const {
    token: { colorBgContainer, borderRadiusLG },
  } = theme.useToken();

  const { isAuth } = useAuth();
  const [collapsed, setCollapsed] = useState(false);

  useLayoutEffect(() => {
    const collapsedSider = localStorage.getItem(COLLAPSED_SIDER);

    if (collapsedSider === 'false') {
      setCollapsed(false);
    } else if (collapsedSider === 'true') {
      setCollapsed(true);
    } else {
      setCollapsed(breakpoints.md ? false : true);
    }

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const updateCollapsed = useCallback((value: boolean) => {
    setCollapsed(value);
    localStorage.setItem(COLLAPSED_SIDER, `${value}`);
  }, []);

  return (
    <>
      <Header collapsed={collapsed} setCollapsed={updateCollapsed} />
      <Layout>
        {isAuth && (
          <Sider collapsed={collapsed} setCollapsed={updateCollapsed} />
        )}
        <Layout style={breakpoints.md ? { padding: '0 24px' } : undefined}>
          <Layout.Content
            style={{
              padding: breakpoints.lg ? 24 : 12,
              marginTop: breakpoints.md ? 16 : undefined,
              background: colorBgContainer,
              borderRadius: breakpoints.md ? borderRadiusLG : undefined,
            }}
          >
            <Outlet />
          </Layout.Content>
          <Footer />
        </Layout>
      </Layout>
    </>
  );
}
