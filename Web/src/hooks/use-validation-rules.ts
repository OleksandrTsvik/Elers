import { Rule } from 'antd/es/form';
import { useTranslation } from 'react-i18next';

import { FILE_SIZE_LIMIT } from '../utils/constants/app.constants';
import { isArrayOf, isObjectWithFileSize, isString } from '../utils/helpers';

export default function useValidationRules() {
  const { t } = useTranslation();

  const trimWhitespace: Rule = {
    validator: (_, value) => {
      if (!isString(value)) {
        return Promise.resolve();
      }

      return value.length === value.trim().length
        ? Promise.resolve()
        : Promise.reject(new Error(t('validation_rule.trim_whitespace')));
    },
  };

  const fileSizeLimit = (sizeLimit: number = FILE_SIZE_LIMIT): Rule => ({
    validator: (_, value) => {
      if (
        !Array.isArray(value) ||
        !value.length ||
        !isArrayOf<{ size: number }>(value, isObjectWithFileSize)
      ) {
        return Promise.resolve();
      }

      return value.every((item) => item.size < sizeLimit)
        ? Promise.resolve()
        : Promise.reject(new Error(t('validation_rule.file_size_limit')));
    },
  });

  return { trimWhitespace, fileSizeLimit };
}
