import { Rule } from 'antd/es/form';

import useValidationRules from '../../hooks/use-validation-rules';

interface Rules {
  text: Rule[];
  files: Rule[];
}

export default function useAssignmentSubmitRules(): Rules {
  const { fileSizeLimit } = useValidationRules();

  return {
    text: [],
    files: [fileSizeLimit()],
  };
}
