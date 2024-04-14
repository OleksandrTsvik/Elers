import { App, ConfigProvider, Layout } from 'antd';
import dayjs from 'dayjs';
import { useEffect } from 'react';

import { configTheme, messageConfig, notificationConfig } from './antd.config';
import { locales } from './antd.locales';
import useColorMode from '../../hooks/use-color-mode';
import useLocale from '../../hooks/use-locale';

interface Props {
  children: React.ReactNode;
}

export default function AntdProvider({ children }: Props) {
  const { locale } = useLocale();
  const { colorMode } = useColorMode();

  useEffect(() => {
    dayjs.locale(locales[locale].dayjs);
  }, [locale]);

  return (
    <ConfigProvider
      theme={configTheme(colorMode)}
      locale={locales[locale].antd}
      direction={locales[locale].direction}
    >
      <App message={messageConfig} notification={notificationConfig}>
        <Layout style={{ minHeight: '100vh' }}>{children}</Layout>
      </App>
    </ConfigProvider>
  );
}
