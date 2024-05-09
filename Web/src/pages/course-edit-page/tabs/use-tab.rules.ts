import { Rule } from 'antd/es/form';
import { useTranslation } from 'react-i18next';

import { COURSE_RULES } from '../../../common/rules';

interface Rules {
  tabName: Rule[];
}

export default function useTabRules(): Rules {
  const { t } = useTranslation();

  return {
    tabName: [
      {
        required: true,
        min: COURSE_RULES.tabName.min,
        max: COURSE_RULES.tabName.max,
        message: t('course.rules.tab_name_len', COURSE_RULES.tabName),
      },
    ],
  };
}
