import { Spin } from 'antd';
import { Suspense } from 'react';

import I18nMiddleware from './i18n.middleware';

interface Props {
  children: React.ReactNode;
}

export default function I18nProvider({ children }: Props) {
  return (
    <Suspense fallback={<Spin fullscreen />}>
      <I18nMiddleware>{children}</I18nMiddleware>
    </Suspense>
  );
}
