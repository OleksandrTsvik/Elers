import { useCallback } from 'react';
import { useTranslation } from 'react-i18next';

import parseErrorObject from '../utils/helpers/parse-error-object.util';

export default function useErrorMessage() {
  const { t } = useTranslation();

  const getErrorMessage = useCallback(
    (error: unknown): string | string[] => {
      const errorObject = parseErrorObject(error);

      let errorMessage: string | string[] = t('error.simple');

      if (errorObject.message) {
        errorMessage = errorObject.message;
      } else if (errorObject.description) {
        errorMessage = errorObject.description;
      } else if (errorObject.status) {
        errorMessage = t('error.status', { status: errorObject.status });
      }

      return errorMessage;
    },
    [t],
  );

  return { getErrorMessage };
}
