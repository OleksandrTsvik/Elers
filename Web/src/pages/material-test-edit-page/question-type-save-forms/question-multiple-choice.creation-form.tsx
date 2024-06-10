import { App } from 'antd';
import { useTranslation } from 'react-i18next';

import { useCreateTestQuestionMultipleChoiceMutation } from '../../../api/test-questions.api';
import QuestionMultipleChoiceForm, {
  QuestionMultipleChoiceFormValues,
} from '../question-type-forms/question-multiple-choice.form';

interface Props {
  testId: string;
}

export default function QuestionMultipleChoiceCreationForm({ testId }: Props) {
  const { t } = useTranslation();
  const { notification } = App.useApp();

  const [createTestQuestionMultipleChoice, { isLoading, error }] =
    useCreateTestQuestionMultipleChoiceMutation();

  const handleSubmit = async (values: QuestionMultipleChoiceFormValues) => {
    await createTestQuestionMultipleChoice({ testId, ...values })
      .unwrap()
      .then(() =>
        notification.success({
          message: t('course_test.notification.question_created'),
        }),
      );
  };

  return (
    <QuestionMultipleChoiceForm
      initialValues={{ text: '', points: 1, options: [] }}
      textOnSubmitButton={t('actions.add')}
      isLoading={isLoading}
      error={error}
      onSubmit={handleSubmit}
    />
  );
}
