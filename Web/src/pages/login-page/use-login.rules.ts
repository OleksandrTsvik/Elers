import { Rule } from 'antd/es/form';
import { useTranslation } from 'react-i18next';

import { USER_RULES } from '../../shared/rules';

interface Rules {
  email: Rule[];
  password: Rule[];
}

export default function useLoginRules(): Rules {
  const { t } = useTranslation();

  return {
    email: [
      {
        required: true,
        type: 'email',
        message: t('login_page.rules.email_required'),
      },
      {
        max: USER_RULES.email.max,
        message: t('users_page.rules.email_len', USER_RULES.email),
      },
    ],
    password: [
      {
        required: true,
        message: t('login_page.rules.password_required'),
      },
      {
        min: USER_RULES.password.min,
        max: USER_RULES.password.max,
        message: t('login_page.rules.password_len', USER_RULES.password),
      },
    ],
  };
}
