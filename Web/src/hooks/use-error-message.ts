import { useTranslation } from 'react-i18next';

import parseErrorObject from '../utils/helpers/parse-error-object.util';

export default function useErrorMessage() {
  const { t } = useTranslation();

  const getErrorMessage = (error: unknown) => {
    const errorObject = parseErrorObject(error);

    const description = errorObject.description;
    let message: string | string[] = t('error.simple');

    if (errorObject.message) {
      message = errorObject.message;
    } else if (errorObject.status) {
      message = t('error.status', { status: errorObject.status });
    }

    return { message, description };
  };

  return { getErrorMessage };
}
