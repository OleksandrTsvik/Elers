import { App } from 'antd';
import { useTranslation } from 'react-i18next';

import { useUpdateTestQuestionInputMutation } from '../../../api/test-questions.api';
import QuestionInputForm, {
  QuestionInputFormValues,
} from '../question-type-forms/question-input.form';

interface Props {
  questionId: string;
  initialValues: QuestionInputFormValues;
}

export default function QuestionInputEditForm({
  questionId,
  initialValues,
}: Props) {
  const { t } = useTranslation();
  const { notification } = App.useApp();

  const [updateTestQuestionInput, { isLoading, error }] =
    useUpdateTestQuestionInputMutation();

  const handleSubmit = async (values: QuestionInputFormValues) => {
    await updateTestQuestionInput({ id: questionId, ...values })
      .unwrap()
      .then(() =>
        notification.success({
          message: t('course_test.notification.question_updated'),
        }),
      );
  };

  return (
    <QuestionInputForm
      initialValues={initialValues}
      textOnSubmitButton={t('actions.save_changes')}
      isLoading={isLoading}
      error={error}
      onSubmit={handleSubmit}
    />
  );
}
