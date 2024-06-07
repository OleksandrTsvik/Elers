import { Rule } from 'antd/es/form';

import { ASSIGNMENT_RULES } from '../../common/rules';

interface Rules {
  status: Rule[];
  grade: Rule[];
  comment: Rule[];
}

export default function useGradeAssignmentRules(maxGrade: number): Rules {
  return {
    status: [
      {
        required: true,
      },
    ],
    grade: [
      {
        required: true,
      },
      {
        type: 'number',
        min: 0,
        max: maxGrade,
      },
    ],
    comment: [{ max: ASSIGNMENT_RULES.comment.max }],
  };
}
