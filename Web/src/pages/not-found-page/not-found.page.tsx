import { Button, Result } from 'antd';
import { useTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';

import NotFoundHead from './not-found.head';

export default function NotFoundPage() {
  const { t } = useTranslation();

  return (
    <>
      <NotFoundHead />
      <Result
        status="404"
        title={t('not_found_page.title')}
        subTitle={t('not_found_page.sub_title')}
        extra={
          <Link to="/">
            <Button type="default">{t('not_found_page.home_link')}</Button>
          </Link>
        }
      />
    </>
  );
}
