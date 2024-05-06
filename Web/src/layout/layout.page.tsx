import { Grid, Layout } from 'antd';
import { useLayoutEffect, useState } from 'react';

import Footer from './footer';
import Header from './header';
import Main from './main';
import Sider from './sider';
import useAuth from '../hooks/use-auth';
import { COLLAPSED_SIDER } from '../utils/constants/local-storage.constants';

import './layout.scss';

export default function LayoutPage() {
  const breakpoints = Grid.useBreakpoint();

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

  const updateCollapsed = (value: boolean) => {
    setCollapsed(value);
    localStorage.setItem(COLLAPSED_SIDER, `${value}`);
  };

  return (
    <>
      <Header collapsed={collapsed} setCollapsed={updateCollapsed} />
      <Layout>
        {isAuth && (
          <Sider collapsed={collapsed} setCollapsed={updateCollapsed} />
        )}
        <Layout style={breakpoints.md ? { padding: '0 24px' } : undefined}>
          <Main />
          <Footer />
        </Layout>
      </Layout>
    </>
  );
}
