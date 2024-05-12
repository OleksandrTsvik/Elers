import { Rule } from 'antd/es/form';
import { useTranslation } from 'react-i18next';

import { USER_RULES } from '../../common/rules';
import { FormMode } from '../../common/types';
import useValidationRules from '../../hooks/use-validation-rules';

interface Rules {
  email: Rule[];
  password: Rule[];
  firstName: Rule[];
  lastName: Rule[];
  patronymic: Rule[];
}

export default function useUserRules(mode: FormMode): Rules {
  const { t } = useTranslation();
  const { trimWhitespace } = useValidationRules();

  const rules: Rules = {
    email: [
      {
        required: true,
        type: 'email',
        message: t('users_page.rules.email_required'),
      },
      {
        max: USER_RULES.email.max,
        message: t('users_page.rules.email_len', USER_RULES.email),
      },
    ],
    password: [
      {
        min: USER_RULES.password.min,
        max: USER_RULES.password.max,
        message: t('users_page.rules.password_len', USER_RULES.password),
      },
    ],
    firstName: [
      {
        min: USER_RULES.firstName.min,
        max: USER_RULES.firstName.max,
        message: t('users_page.rules.firstName_len', USER_RULES.firstName),
      },
      trimWhitespace,
    ],
    lastName: [
      {
        min: USER_RULES.lastName.min,
        max: USER_RULES.lastName.max,
        message: t('users_page.rules.lastName_len', USER_RULES.lastName),
      },
      trimWhitespace,
    ],
    patronymic: [
      {
        min: USER_RULES.patronymic.min,
        max: USER_RULES.patronymic.max,
        message: t('users_page.rules.patronymic_len', USER_RULES.patronymic),
      },
      trimWhitespace,
    ],
  };

  switch (mode) {
    case FormMode.Creation:
      rules.password.push({
        required: true,
        message: t('users_page.rules.password_required'),
      });
      break;
  }

  return rules;
}
