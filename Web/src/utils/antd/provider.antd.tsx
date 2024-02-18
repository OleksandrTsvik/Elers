import { App, ConfigProvider, Layout } from 'antd';
import dayjs from 'dayjs';
import { useMemo } from 'react';

import configTheme from './config-theme.antd';
import { Locale, locales } from './locales.antd';
import useColorMode from '../../hooks/use-color-mode';
import useLocale from '../../hooks/use-locale';

interface Props {
  children: React.ReactNode;
}

export default function AntdProvider({ children }: Props) {
  const { locale } = useLocale();

  const componentsLocale: Locale = useMemo(() => {
    const localeSettings = locales[locale];

    dayjs.locale(localeSettings.dayjs);

    return localeSettings.antd;
  }, [locale]);

  const { colorMode } = useColorMode();
  const theme = useMemo(() => configTheme(colorMode), [colorMode]);

  return (
    <ConfigProvider theme={theme} locale={componentsLocale}>
      <App>
        <Layout style={{ minHeight: '100vh' }}>{children}</Layout>
      </App>
    </ConfigProvider>
  );
}
