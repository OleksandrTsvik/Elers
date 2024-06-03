import { Rule } from 'antd/es/form';

import { COURSE_MATERIAL_RULES } from '../../common/rules';
import useValidationRules from '../../hooks/use-validation-rules';

interface Rules {
  title: Rule[];
  description: Rule[];
  deadline: Rule[];
  maxFiles: Rule[];
  maxGrade: Rule[];
}

export default function useMaterialAssignmentRules(): Rules {
  const { trimWhitespace } = useValidationRules();

  return {
    title: [
      {
        required: true,
      },
      {
        min: COURSE_MATERIAL_RULES.assignment.title.min,
        max: COURSE_MATERIAL_RULES.assignment.title.max,
      },
      trimWhitespace,
    ],
    description: [
      {
        required: true,
      },
    ],
    deadline: [],
    maxFiles: [
      {
        required: true,
      },
      {
        type: 'integer',
        min: COURSE_MATERIAL_RULES.assignment.maxFiles.min,
        max: COURSE_MATERIAL_RULES.assignment.maxFiles.max,
      },
    ],
    maxGrade: [
      {
        required: true,
      },
      {
        type: 'integer',
        min: COURSE_MATERIAL_RULES.assignment.maxGrade.min,
        max: COURSE_MATERIAL_RULES.assignment.maxGrade.max,
      },
    ],
  };
}
