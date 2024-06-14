import { Rule } from 'antd/es/form';
import { useTranslation } from 'react-i18next';

import { USER_RULES } from '../../common/rules';

interface Rules {
  currentPassword: Rule[];
  newPassword: Rule[];
  confirmPassword: Rule[];
}

export default function usePasswordRules(): Rules {
  const { t } = useTranslation();

  return {
    currentPassword: [
      {
        required: true,
      },
    ],
    newPassword: [
      {
        required: true,
      },
      {
        min: USER_RULES.password.min,
        max: USER_RULES.password.max,
        message: t('users_page.rules.password_len', USER_RULES.password),
      },
    ],
    confirmPassword: [
      {
        required: true,
      },
      ({ getFieldValue }) => ({
        validator: (_, value) => {
          if (!value || getFieldValue('newPassword') === value) {
            return Promise.resolve();
          }

          return Promise.reject(
            new Error(t('my_profile_page.passwords_do_not_match')),
          );
        },
      }),
    ],
  };
}
