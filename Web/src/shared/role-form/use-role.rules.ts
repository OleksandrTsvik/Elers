import { Rule } from 'antd/es/form';
import { useTranslation } from 'react-i18next';

import { ROLE_RULES } from '../../common/rules';
import useValidationRules from '../../hooks/use-validation-rules';

interface Rules {
  name: Rule[];
}

export default function useRoleRules(): Rules {
  const { t } = useTranslation();
  const { trimWhitespace } = useValidationRules();

  return {
    name: [
      {
        required: true,
        message: t('roles_page.rules.name_required'),
      },
      {
        min: ROLE_RULES.name.min,
        max: ROLE_RULES.name.max,
        message: t('roles_page.rules.name_len', ROLE_RULES.name),
      },
      trimWhitespace,
    ],
  };
}
