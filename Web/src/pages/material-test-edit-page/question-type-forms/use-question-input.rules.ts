import { Rule } from 'antd/es/form';

interface Rules {
  answer: Rule[];
}

export default function useQuestionInputRules(): Rules {
  return {
    answer: [
      {
        required: true,
      },
    ],
  };
}
