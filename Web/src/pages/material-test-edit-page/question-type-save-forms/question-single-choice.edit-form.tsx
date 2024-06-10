import { App } from 'antd';
import { useTranslation } from 'react-i18next';

import { useUpdateTestQuestionSingleChoiceMutation } from '../../../api/test-questions.api';
import QuestionSingleChoiceForm, {
  QuestionSingleChoiceFormValues,
} from '../question-type-forms/question-single-choice.form';

interface Props {
  questionId: string;
  initialValues: QuestionSingleChoiceFormValues;
}

export default function QuestionSingleChoiceEditForm({
  questionId,
  initialValues,
}: Props) {
  const { t } = useTranslation();
  const { notification } = App.useApp();

  const [updateTestQuestionSingleChoice, { isLoading, error }] =
    useUpdateTestQuestionSingleChoiceMutation();

  const handleSubmit = async (values: QuestionSingleChoiceFormValues) => {
    await updateTestQuestionSingleChoice({ id: questionId, ...values })
      .unwrap()
      .then(() =>
        notification.success({
          message: t('course_test.notification.question_updated'),
        }),
      );
  };

  return (
    <QuestionSingleChoiceForm
      initialValues={initialValues}
      textOnSubmitButton={t('actions.save_changes')}
      isLoading={isLoading}
      error={error}
      onSubmit={handleSubmit}
    />
  );
}
