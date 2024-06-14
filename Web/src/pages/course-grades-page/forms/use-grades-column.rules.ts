import { Rule } from 'antd/es/form';

import { GRADE_RULES } from '../../../common/rules';
import useValidationRules from '../../../hooks/use-validation-rules';

interface Rules {
  title: Rule[];
  date: Rule[];
  maxGrade: Rule[];
}

export default function useGradesColumnRules(): Rules {
  const { trimWhitespace } = useValidationRules();

  return {
    title: [
      {
        required: true,
      },
      {
        min: GRADE_RULES.title.min,
        max: GRADE_RULES.title.max,
      },
      trimWhitespace,
    ],
    maxGrade: [
      {
        required: true,
      },
      {
        type: 'integer',
        min: GRADE_RULES.maxGrade.min,
        max: GRADE_RULES.maxGrade.max,
      },
    ],
    date: [
      {
        required: true,
        type: 'date',
      },
    ],
  };
}
