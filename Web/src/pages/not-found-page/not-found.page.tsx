import { Button, Result } from 'antd';
import { useTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';

import NotFoundHead from './not-found.head';
import useLocationFrom from '../../hooks/use-location-from';

export default function NotFoundPage() {
  const { t } = useTranslation();
  const { locationFrom, hasFromLocation } = useLocationFrom();

  return (
    <>
      <NotFoundHead />
      <Result
        status="404"
        title={t('not_found_page.title')}
        subTitle={t('not_found_page.sub_title')}
        extra={
          <>
            <Link to="/">
              <Button type="default">{t('not_found_page.home_link')}</Button>
            </Link>
            {hasFromLocation && (
              <Link to={locationFrom}>
                <Button type="default">{t('not_found_page.go_back')}</Button>
              </Link>
            )}
          </>
        }
      />
    </>
  );
}
