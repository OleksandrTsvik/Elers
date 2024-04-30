import { Rule } from 'antd/es/form';
import { useTranslation } from 'react-i18next';

interface Rules {
  name: Rule[];
}

export default function useRoleRules(): Rules {
  const { t } = useTranslation();

  return {
    name: [
      {
        required: true,
        message: t('roles_page.rules.name_required'),
      },
      {
        min: 2,
        max: 32,
        message: t('roles_page.rules.name_len', { min: 2, max: 32 }),
      },
    ],
  };
}
