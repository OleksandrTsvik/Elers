import { Button, Result } from 'antd';
import { useTranslation } from 'react-i18next';
import { Link, useLocation } from 'react-router-dom';

import ErrorHead from './error.head';
import useParseError from '../../hooks/use-parse-error';

export type LocationStateError = { error: unknown } | undefined;

export default function ErrorPage() {
  const { t } = useTranslation();
  const location = useLocation();

  const error = (location.state as LocationStateError)?.error;
  const { status, message, description } = useParseError(error);

  return (
    <>
      <ErrorHead title={message} />
      <Result
        status={
          status === 403 || status === 404 || status === 500 ? status : 'error'
        }
        title={message}
        subTitle={description}
        extra={
          <Link to="/">
            <Button type="default">{t('error_page.home_link')}</Button>
          </Link>
        }
      />
    </>
  );
}
