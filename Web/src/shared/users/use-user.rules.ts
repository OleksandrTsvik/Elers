import { Rule } from 'antd/es/form';
import { useTranslation } from 'react-i18next';

interface Rules {
  email: Rule[];
  password: Rule[];
  firstName: Rule[];
  lastName: Rule[];
  patronymic: Rule[];
}

const RULES = {
  password: { min: 6, max: 32 },
  firstName: { min: 1, max: 64 },
  lastName: { min: 1, max: 64 },
  patronymic: { min: 1, max: 64 },
};

export default function useUserRules(): Rules {
  const { t } = useTranslation();

  return {
    email: [
      {
        required: true,
        type: 'email',
        message: t('users_page.rules.email_required'),
      },
    ],
    password: [
      {
        required: true,
        message: t('users_page.rules.password_required'),
      },
      {
        min: RULES.password.min,
        max: RULES.password.max,
        message: t('users_page.rules.password_len', RULES.password),
      },
    ],
    firstName: [
      {
        min: RULES.firstName.min,
        max: RULES.firstName.max,
        message: t('users_page.rules.firstName_len', RULES.firstName),
      },
    ],
    lastName: [
      {
        min: RULES.lastName.min,
        max: RULES.lastName.max,
        message: t('users_page.rules.lastName_len', RULES.lastName),
      },
    ],
    patronymic: [
      {
        min: RULES.patronymic.min,
        max: RULES.patronymic.max,
        message: t('users_page.rules.patronymic_len', RULES.patronymic),
      },
    ],
  };
}
