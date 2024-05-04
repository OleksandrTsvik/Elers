import { Rule } from 'antd/es/form';
import { useTranslation } from 'react-i18next';

import { COURSE_RULES } from '../../shared/rules';

interface Rules {
  title: Rule[];
  description: Rule[];
}

export default function useCourseCreationRules(): Rules {
  const { t } = useTranslation();

  return {
    title: [
      {
        required: true,
        message: t('course.rules.title_required'),
      },
      {
        min: COURSE_RULES.title.min,
        max: COURSE_RULES.title.max,
        message: t('course.rules.title_len', COURSE_RULES.title),
      },
    ],
    description: [
      {
        max: COURSE_RULES.description.max,
        message: t('course.rules.description_len', COURSE_RULES.description),
      },
    ],
  };
}
