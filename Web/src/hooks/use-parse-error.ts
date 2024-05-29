import { TFunction } from 'i18next';
import { useTranslation } from 'react-i18next';

import { IS_DEVELOPMENT } from '../utils/constants/node-env.constants';
import { ValidationErrors, isNumber, parseErrorObject } from '../utils/helpers';

interface ErrorData {
  status?: number;
  message: string;
  description: string[];
  validation?: ValidationErrors;
}

export default function useParseError(error: unknown): ErrorData {
  const { t } = useTranslation();

  const errorObject = parseErrorObject(error);

  let status: number | undefined = undefined;
  let message: string = t('error.simple');
  let description: string[] = [];

  if (isNumber(errorObject.status)) {
    status = errorObject.status;
  }

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

  return { status, message, description, validation: errorObject.validation };
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

  if (IS_DEVELOPMENT) {
    return t('error.status', { status: statusCode });
  }

  return t('error.default');
}
