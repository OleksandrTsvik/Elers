import { TFunction } from 'i18next';
import { useTranslation } from 'react-i18next';

import parseErrorObject from '../utils/helpers/parse-error-object.util';
import { isNumber } from '../utils/helpers/type-guards.util';

export default function useErrorMessage() {
  const { t } = useTranslation();

  const getErrorMessage = (error: unknown) => {
    const errorObject = parseErrorObject(error);

    const description = errorObject.description;
    let message: string | string[] = t('error.simple');

    if (errorObject.message) {
      message = errorObject.message;
    } else if (errorObject.status) {
      message = getErrorMessageByStatus(t, errorObject.status);
    }

    return { message, description };
  };

  return { getErrorMessage };
}

function getErrorMessageByStatus(
  t: TFunction<'translation', undefined>,
  statusCode: unknown,
): string {
  if (isNumber(statusCode)) {
    switch (statusCode) {
      case 401:
        return t('error.unauthorized');
      case 403:
        return t('error.forbidden');
    }
  }

  return t('error.status', { status: statusCode });
}
