import { Rule } from 'antd/es/form';
import { useTranslation } from 'react-i18next';

import { FormMode } from '../../models/form-mode.enum';
import { USER_RULES } from '../rules';

interface Rules {
  email: Rule[];
  password: Rule[];
  firstName: Rule[];
  lastName: Rule[];
  patronymic: Rule[];
}

export default function useUserRules(mode: FormMode): Rules {
  const { t } = useTranslation();

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
    ],
    lastName: [
      {
        min: USER_RULES.lastName.min,
        max: USER_RULES.lastName.max,
        message: t('users_page.rules.lastName_len', USER_RULES.lastName),
      },
    ],
    patronymic: [
      {
        min: USER_RULES.patronymic.min,
        max: USER_RULES.patronymic.max,
        message: t('users_page.rules.patronymic_len', USER_RULES.patronymic),
      },
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
