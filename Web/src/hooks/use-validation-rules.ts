import { Rule } from 'antd/es/form';
import { useTranslation } from 'react-i18next';

export default function useValidationRules() {
  const { t } = useTranslation();

  const trimWhitespace: Rule = {
    validator: (_, value) => {
      if (typeof value !== 'string') {
        return Promise.resolve();
      }

      return value.length === value.trim().length
        ? Promise.resolve()
        : Promise.reject(new Error(t('validation_rule.trim_whitespace')));
    },
  };

  return { trimWhitespace };
}
