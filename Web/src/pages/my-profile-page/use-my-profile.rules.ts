import { Rule } from 'antd/es/form';
import { useTranslation } from 'react-i18next';

import { USER_RULES } from '../../common/rules';
import useValidationRules from '../../hooks/use-validation-rules';

interface Rules {
  email: Rule[];
  firstName: Rule[];
  lastName: Rule[];
  patronymic: Rule[];
  birthDate: Rule[];
}

export default function useMyProfileRules(): Rules {
  const { t } = useTranslation();
  const { trimWhitespace } = useValidationRules();

  return {
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
    firstName: [
      {
        required: true,
      },
      {
        min: USER_RULES.firstName.min,
        max: USER_RULES.firstName.max,
        message: t('users_page.rules.firstName_len', USER_RULES.firstName),
      },
      trimWhitespace,
    ],
    lastName: [
      {
        required: true,
      },
      {
        min: USER_RULES.lastName.min,
        max: USER_RULES.lastName.max,
        message: t('users_page.rules.lastName_len', USER_RULES.lastName),
      },
      trimWhitespace,
    ],
    patronymic: [
      {
        required: true,
      },
      {
        min: USER_RULES.patronymic.min,
        max: USER_RULES.patronymic.max,
        message: t('users_page.rules.patronymic_len', USER_RULES.patronymic),
      },
      trimWhitespace,
    ],
    birthDate: [{ type: 'date' }],
  };
}
