export type TestQuestion = {
  id: string;
  testId: string;
  text: string;
  points: number;
  createdAt: Date;
} & ConditionalTestQuestion;

export type ConditionalTestQuestion =
  | {
      type: TestQuestionType.Input;
      answer: string;
    }
  | {
      type: TestQuestionType.SingleChoice;
      options: TestQuestionChoiceOption[];
    }
  | {
      type: TestQuestionType.MultipleChoice;
      options: TestQuestionChoiceOption[];
    }
  | {
      type: TestQuestionType.Matching;
      options: TestQuestionMatchOption[];
    };

export enum TestQuestionType {
  Input = 'Input',
  SingleChoice = 'SingleChoice',
  MultipleChoice = 'MultipleChoice',
  Matching = 'Matching',
}

export interface TestQuestionChoiceOption {
  option: string;
  isCorrect: boolean;
}

export interface TestQuestionMatchOption {
  question?: string;
  answer: string;
}
