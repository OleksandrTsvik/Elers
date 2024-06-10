import { App } from 'antd';
import { useTranslation } from 'react-i18next';

import { useUpdateTestQuestionMultipleChoiceMutation } from '../../../api/test-questions.api';
import QuestionMultipleChoiceForm, {
  QuestionMultipleChoiceFormValues,
} from '../question-type-forms/question-multiple-choice.form';

interface Props {
  questionId: string;
  initialValues: QuestionMultipleChoiceFormValues;
}

export default function QuestionMultipleChoiceEditForm({
  questionId,
  initialValues,
}: Props) {
  const { t } = useTranslation();
  const { notification } = App.useApp();

  const [updateTestQuestionMultipleChoice, { isLoading, error }] =
    useUpdateTestQuestionMultipleChoiceMutation();

  const handleSubmit = async (values: QuestionMultipleChoiceFormValues) => {
    await updateTestQuestionMultipleChoice({ id: questionId, ...values })
      .unwrap()
      .then(() =>
        notification.success({
          message: t('course_test.notification.question_updated'),
        }),
      );
  };

  return (
    <QuestionMultipleChoiceForm
      initialValues={initialValues}
      textOnSubmitButton={t('actions.save_changes')}
      isLoading={isLoading}
      error={error}
      onSubmit={handleSubmit}
    />
  );
}
