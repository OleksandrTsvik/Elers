import { TestQuestionType } from './test-question.interface';

export interface Test {
  testId: string;
  courseTabId: string;
  title: string;
  description?: string;
  numberAttempts: number;
  timeLimitInMinutes?: number;
  deadline?: Date;
  attempts: TestAttemptItem[];
}

export interface TestAttemptItem {
  testSessionId: string;
  startedAt: Date;
  finishedAt?: Date;
  grade?: number;
  isCompleted: boolean;
}

export interface TestSession {
  testSessionId: string;
  testId: string;
  startedAt: Date;
  timeLimitInMinutes?: number;
  questions: TestSessionQuestionItem[];
}

export interface TestSessionQuestionItem {
  questionId: string;
  isAnswered: boolean;
}

export type TestSessionQuestion = {
  questionId: string;
  questionText: string;
  points: number;
} & (
  | {
      questionType: TestQuestionType.Input;
      userAnswer?: string;
    }
  | {
      questionType: TestQuestionType.SingleChoice;
      options: string[];
      userAnswer?: string;
    }
  | {
      questionType: TestQuestionType.MultipleChoice;
      options: string[];
      userAnswers?: string[];
    }
);
