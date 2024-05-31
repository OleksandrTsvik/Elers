import { Rule } from 'antd/es/form';

import { COURSE_ROLE_RULES } from '../../common/rules';
import useValidationRules from '../../hooks/use-validation-rules';

interface Rules {
  name: Rule[];
}

export default function useCourseRoleRules(): Rules {
  const { trimWhitespace } = useValidationRules();

  return {
    name: [
      {
        required: true,
      },
      {
        min: COURSE_ROLE_RULES.name.min,
        max: COURSE_ROLE_RULES.name.max,
      },
      trimWhitespace,
    ],
  };
}
