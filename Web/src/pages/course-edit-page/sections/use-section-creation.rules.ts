import { Rule } from 'antd/es/form';
import { useTranslation } from 'react-i18next';

import { COURSE_RULES } from '../../../shared/rules';

interface Rules {
  tabName: Rule[];
}

export default function useSectionCreationRules(): Rules {
  const { t } = useTranslation();

  return {
    tabName: [
      {
        required: true,
        min: COURSE_RULES.tabName.min,
        max: COURSE_RULES.tabName.max,
        message: t('course.rules.section_name_len', COURSE_RULES.tabName),
      },
    ],
  };
}
