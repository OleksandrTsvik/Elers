import { Rule } from 'antd/es/form';

export interface TestQuestionFormValues {
  text: string;
  points: number;
}

export interface OptionChoiceValue {
  option: string;
  isCorrect: boolean;
}

interface Rules {
  text: Rule[];
  points: Rule[];
}

export function useBaseTestQuestionRules(): Rules {
  return {
    text: [
      {
        required: true,
      },
    ],
    points: [
      {
        required: true,
      },
      {
        type: 'number',
        min: 0.1,
      },
    ],
  };
}
