import QuestionInputCreationForm from './question-type-save-forms/question-input.creation-form';
import QuestionMatchingCreationForm from './question-type-save-forms/question-matching.creation-form';
import QuestionMultipleChoiceCreationForm from './question-type-save-forms/question-multiple-choice.creation-form';
import QuestionSingleChoiceCreationForm from './question-type-save-forms/question-single-choice.creation-form';
import TestQuestionBodyEditForm from './test-question.body-edit-form';
import { TestQuestionType } from '../../models/test-question.interface';

interface Props {
  testId: string;
  questionId: string | undefined;
  questionType: TestQuestionType;
}

export default function TestQuestionBodyForm({
  testId,
  questionId,
  questionType,
}: Props) {
  if (!questionId) {
    switch (questionType) {
      case TestQuestionType.Input:
        return <QuestionInputCreationForm testId={testId} />;
      case TestQuestionType.SingleChoice:
        return <QuestionSingleChoiceCreationForm testId={testId} />;
      case TestQuestionType.MultipleChoice:
        return <QuestionMultipleChoiceCreationForm testId={testId} />;
      case TestQuestionType.Matching:
        return <QuestionMatchingCreationForm testId={testId} />;
      default:
        return null;
    }
  }

  return <TestQuestionBodyEditForm questionId={questionId} />;
}
