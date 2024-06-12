import { Rule } from 'antd/es/form';

import { COURSE_MATERIAL_RULES } from '../../common/rules';
import useValidationRules from '../../hooks/use-validation-rules';
import { GradingMethod } from '../../models/course-material.type';

interface Rules {
  title: Rule[];
  description: Rule[];
  numberAttempts: Rule[];
  timeLimitInMinutes: Rule[];
  deadline: Rule[];
  gradingMethod: Rule[];
  shuffleQuestions: Rule[];
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
        required: true,
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
    gradingMethod: [
      {
        required: true,
      },
      {
        type: 'enum',
        enum: Object.values(GradingMethod),
      },
    ],
    shuffleQuestions: [
      {
        required: true,
        type: 'boolean',
      },
    ],
  };
}
