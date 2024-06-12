import TestQuestionInputFrom from './test-question-forms/test-question-input.form';
import TestQuestionMatchingFrom from './test-question-forms/test-question-matching.form';
import TestQuestionMultipleChoiceFrom from './test-question-forms/test-question-multiple-choice.form';
import TestQuestionSingleChoiceFrom from './test-question-forms/test-question-single-choice.form';
import { TestQuestionType } from '../../models/test-question.interface';
import { TestSessionQuestion } from '../../models/test.interface';

interface Props {
  testSessionQuestion: TestSessionQuestion;
}

export default function TestQuestionForm({ testSessionQuestion }: Props) {
  switch (testSessionQuestion.questionType) {
    case TestQuestionType.Input:
      return (
        <TestQuestionInputFrom
          questionId={testSessionQuestion.questionId}
          userAnswer={testSessionQuestion.userAnswer}
        />
      );
    case TestQuestionType.SingleChoice:
      return (
        <TestQuestionSingleChoiceFrom
          questionId={testSessionQuestion.questionId}
          options={testSessionQuestion.options}
          userAnswer={testSessionQuestion.userAnswer}
        />
      );
    case TestQuestionType.MultipleChoice:
      return (
        <TestQuestionMultipleChoiceFrom
          questionId={testSessionQuestion.questionId}
          options={testSessionQuestion.options}
          userAnswers={testSessionQuestion.userAnswers}
        />
      );
    case TestQuestionType.Matching:
      return (
        <TestQuestionMatchingFrom
          questionId={testSessionQuestion.questionId}
          questions={testSessionQuestion.questions}
          answers={testSessionQuestion.answers}
          userAnswers={testSessionQuestion.userAnswers}
        />
      );
    default:
      return null;
  }
}
