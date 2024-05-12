import { Rule } from 'antd/es/form';
import { useTranslation } from 'react-i18next';

import { COURSE_RULES } from '../../common/rules';
import useValidationRules from '../../hooks/use-validation-rules';

interface Rules {
  title: Rule[];
  description: Rule[];
}

export default function useCourseCreationRules(): Rules {
  const { t } = useTranslation();
  const { trimWhitespace } = useValidationRules();

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
      trimWhitespace,
    ],
    description: [
      {
        max: COURSE_RULES.description.max,
        message: t('course.rules.description_len', COURSE_RULES.description),
      },
      trimWhitespace,
    ],
  };
}
