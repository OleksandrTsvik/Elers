import { TFunction } from 'i18next';
import { useTranslation } from 'react-i18next';

import { isNumber, parseErrorObject } from '../utils/helpers';

export default function useParseError(error: unknown) {
  const { t } = useTranslation();

  const errorObject = parseErrorObject(error);

  let message: string = t('error.simple');
  let description: string[] = [];

  if (errorObject.message) {
    message = errorObject.message;
  } else if (errorObject.status) {
    message = getErrorMessageByStatus(t, errorObject.status);
  }

  if (errorObject.description) {
    description = Array.isArray(errorObject.description)
      ? errorObject.description
      : [errorObject.description];
  }

  return { message, description };
}

function getErrorMessageByStatus(
  t: TFunction<'translation', undefined>,
  statusCode: unknown,
): string {
  if (isNumber(statusCode)) {
    switch (statusCode) {
      case 400:
        return t('error.bad_request');
      case 401:
        return t('error.unauthorized');
      case 403:
        return t('error.forbidden');
      case 404:
        return t('error.not_found');
      case 409:
        return t('error.conflict');
      case 415:
        return t('error.unsupported_media_type');
      case 500:
        return t('error.internal_server_error');
    }
  }

  return t('error.status', { status: statusCode });
}
