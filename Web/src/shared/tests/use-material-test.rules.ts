import { Rule } from 'antd/es/form';

import { COURSE_MATERIAL_RULES } from '../../common/rules';
import useValidationRules from '../../hooks/use-validation-rules';

interface Rules {
  title: Rule[];
  description: Rule[];
  numberAttempts: Rule[];
  timeLimitInMinutes: Rule[];
  deadline: Rule[];
}

export default function useMaterialTestRules(): Rules {
  const { trimWhitespace } = useValidationRules();

  return {
    title: [
      {
        required: true,
      },
      {
        min: COURSE_MATERIAL_RULES.test.title.min,
        max: COURSE_MATERIAL_RULES.test.title.max,
      },
      trimWhitespace,
    ],
    description: [],
    numberAttempts: [
      {
        type: 'integer',
        min: COURSE_MATERIAL_RULES.test.numberAttempts.min,
        max: COURSE_MATERIAL_RULES.test.numberAttempts.max,
      },
    ],
    timeLimitInMinutes: [
      {
        type: 'integer',
        min: COURSE_MATERIAL_RULES.test.timeLimitInMinutes.min,
        max: COURSE_MATERIAL_RULES.test.timeLimitInMinutes.max,
      },
    ],
    deadline: [],
  };
}
