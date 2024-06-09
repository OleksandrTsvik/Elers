import { App } from 'antd';
import { useTranslation } from 'react-i18next';

import { useCreateTestQuestionInputMutation } from '../../../api/test-questions.api';
import QuestionInputForm, {
  QuestionInputFormValues,
} from '../question-type-forms/question-input.form';

interface Props {
  testId: string;
}

export default function QuestionInputCreationForm({ testId }: Props) {
  const { t } = useTranslation();
  const { notification } = App.useApp();

  const [createTestQuestionInput, { isLoading, error }] =
    useCreateTestQuestionInputMutation();

  const handleSubmit = async (values: QuestionInputFormValues) => {
    await createTestQuestionInput({ testId, ...values })
      .unwrap()
      .then(() =>
        notification.success({
          message: t('course_test.notification.question_created'),
        }),
      );
  };

  return (
    <QuestionInputForm
      initialValues={{ text: '', points: 1, answer: '' }}
      textOnSubmitButton={t('actions.add')}
      isLoading={isLoading}
      error={error}
      onSubmit={handleSubmit}
    />
  );
}
