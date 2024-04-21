import { Rule } from 'antd/es/form';
import { useTranslation } from 'react-i18next';

interface Rules {
  name: Rule[];
}

export default function useRoleEditRules(): Rules {
  const { t } = useTranslation();

  return {
    name: [
      {
        required: true,
        message: t('role_edit_page.rules.name_required'),
      },
      {
        min: 2,
        max: 32,
        message: t('role_edit_page.rules.name_len', { min: 2, max: 32 }),
      },
    ],
  };
}
