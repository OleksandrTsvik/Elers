import { useEffect } from 'react';
import { useTranslation } from 'react-i18next';

import './i18n';

interface Props {
  children: React.ReactNode;
}

export default function I18nMiddleware({ children }: Props) {
  const { i18n } = useTranslation();

  useEffect(() => {
    document.documentElement.lang = i18n.language;
    document.documentElement.dir = i18n.dir();

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return <>{children}</>;
}
