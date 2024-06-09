import QuestionMultipleChoiceForm from './question-type-forms/question-multiple-choice.form';
import QuestionSingleChoiceForm from './question-type-forms/question-single-choice.form';
import QuestionInputCreationForm from './question-type-save-forms/question-input.creation-form';
import QuestionInputEditForm from './question-type-save-forms/question-input.edit-form';
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
        return (
          <QuestionSingleChoiceForm
            initialValues={{ text: '', points: 1, options: [] }}
            textOnSubmitButton="Go"
            isLoading={false}
            error={undefined}
            onSubmit={(values) => console.log(values)}
          />
        );
      case TestQuestionType.MultipleChoice:
        return (
          <QuestionMultipleChoiceForm
            initialValues={{ text: '', points: 1, options: [] }}
            textOnSubmitButton="Go"
            isLoading={false}
            error={undefined}
            onSubmit={(values) => console.log(values)}
          />
        );
    }
  }

  switch (questionType) {
    case TestQuestionType.Input:
      return <QuestionInputEditForm questionId={questionId} />;
  }
}
