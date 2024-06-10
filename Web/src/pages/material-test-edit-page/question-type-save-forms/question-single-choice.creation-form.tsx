import { App } from 'antd';
import { useTranslation } from 'react-i18next';

import { useCreateTestQuestionSingleChoiceMutation } from '../../../api/test-questions.api';
import QuestionSingleChoiceForm, {
  QuestionSingleChoiceFormValues,
} from '../question-type-forms/question-single-choice.form';

interface Props {
  testId: string;
}

export default function QuestionSingleChoiceCreationForm({ testId }: Props) {
  const { t } = useTranslation();
  const { notification } = App.useApp();

  const [createTestQuestionSingleChoice, { isLoading, error }] =
    useCreateTestQuestionSingleChoiceMutation();

  const handleSubmit = async (values: QuestionSingleChoiceFormValues) => {
    await createTestQuestionSingleChoice({ testId, ...values })
      .unwrap()
      .then(() =>
        notification.success({
          message: t('course_test.notification.question_created'),
        }),
      );
  };

  return (
    <QuestionSingleChoiceForm
      initialValues={{ text: '', points: 1, options: [] }}
      textOnSubmitButton={t('actions.add')}
      isLoading={isLoading}
      error={error}
      onSubmit={handleSubmit}
    />
  );
}
