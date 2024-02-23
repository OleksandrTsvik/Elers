import { Rule } from 'antd/es/form';
import { useMemo } from 'react';
import { useTranslation } from 'react-i18next';

interface Rules {
  email: Rule[];
  password: Rule[];
}

export default function useLoginRules(): Rules {
  const { t } = useTranslation();

  return useMemo(
    () => ({
      email: [
        {
          required: true,
          type: 'email',
          message: t('login_page.rules.email_required'),
        },
      ],
      password: [
        {
          required: true,
          message: t('login_page.rules.password_required'),
        },
        {
          min: 6,
          max: 32,
          message: t('login_page.rules.password_len', { min: 6, max: 32 }),
        },
      ],
    }),
    [t],
  );
}
