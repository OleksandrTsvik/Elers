import { App } from 'antd';
import { useTranslation } from 'react-i18next';

import { useCreateTestQuestionMatchingMutation } from '../../../api/test-questions.api';
import QuestionMatchingForm, {
  QuestionMatchingFormValues,
} from '../question-type-forms/question-matching.form';

interface Props {
  testId: string;
}

export default function QuestionMatchingCreationForm({ testId }: Props) {
  const { t } = useTranslation();
  const { notification } = App.useApp();

  const [createTestQuestionMatching, { isLoading, error }] =
    useCreateTestQuestionMatchingMutation();

  const handleSubmit = async (values: QuestionMatchingFormValues) => {
    await createTestQuestionMatching({ testId, ...values })
      .unwrap()
      .then(() =>
        notification.success({
          message: t('course_test.notification.question_created'),
        }),
      );
  };

  return (
    <QuestionMatchingForm
      initialValues={{ text: '', points: 1, options: [] }}
      textOnSubmitButton={t('actions.add')}
      isLoading={isLoading}
      error={error}
      onSubmit={handleSubmit}
    />
  );
}
