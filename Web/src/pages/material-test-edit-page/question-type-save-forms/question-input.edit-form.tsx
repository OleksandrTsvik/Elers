import { App, Skeleton, Spin } from 'antd';
import { useTranslation } from 'react-i18next';

import {
  useGetTestQuestionInputQuery,
  useUpdateTestQuestionInputMutation,
} from '../../../api/test-questions.api';
import { NavigateToError, NavigateToNotFound } from '../../../common/navigate';
import QuestionInputForm, {
  QuestionInputFormValues,
} from '../question-type-forms/question-input.form';

interface Props {
  questionId: string;
}

export default function QuestionInputEditForm({ questionId }: Props) {
  const { t } = useTranslation();
  const { notification } = App.useApp();

  const { data, isLoading, isFetching, error } = useGetTestQuestionInputQuery({
    id: questionId,
  });

  const [
    updateTestQuestionInput,
    { isLoading: isLoadingUpdate, error: errorUpdate },
  ] = useUpdateTestQuestionInputMutation();

  const handleSubmit = async (values: QuestionInputFormValues) => {
    await updateTestQuestionInput({ id: questionId, ...values })
      .unwrap()
      .then(() =>
        notification.success({
          message: t('course_test.notification.question_updated'),
        }),
      );
  };

  if (isLoading) {
    return <Skeleton active />;
  }

  if (error) {
    return <NavigateToError error={error} />;
  }

  if (!data) {
    return <NavigateToNotFound />;
  }

  return (
    <Spin spinning={isFetching} tip={t('loading.data')}>
      <QuestionInputForm
        initialValues={data}
        textOnSubmitButton={t('actions.save_changes')}
        isLoading={isLoadingUpdate}
        error={errorUpdate}
        onSubmit={handleSubmit}
      />
    </Spin>
  );
}
