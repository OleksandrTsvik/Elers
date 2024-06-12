import { App } from 'antd';
import { useTranslation } from 'react-i18next';

import { useUpdateTestQuestionMatchingMutation } from '../../../api/test-questions.api';
import QuestionMatchingForm, {
  QuestionMatchingFormValues,
} from '../question-type-forms/question-matching.form';

interface Props {
  questionId: string;
  initialValues: QuestionMatchingFormValues;
}

export default function QuestionMatchingEditForm({
  questionId,
  initialValues,
}: Props) {
  const { t } = useTranslation();
  const { notification } = App.useApp();

  const [updateTestQuestionMatching, { isLoading, error }] =
    useUpdateTestQuestionMatchingMutation();

  const handleSubmit = async (values: QuestionMatchingFormValues) => {
    await updateTestQuestionMatching({ id: questionId, ...values })
      .unwrap()
      .then(() =>
        notification.success({
          message: t('course_test.notification.question_updated'),
        }),
      );
  };

  return (
    <QuestionMatchingForm
      initialValues={initialValues}
      textOnSubmitButton={t('actions.save_changes')}
      isLoading={isLoading}
      error={error}
      onSubmit={handleSubmit}
    />
  );
}
