import { Skeleton, Spin } from 'antd';
import { useTranslation } from 'react-i18next';

import QuestionInputEditForm from './question-type-save-forms/question-input.edit-form';
import QuestionMatchingEditForm from './question-type-save-forms/question-mathcing.edit-form';
import QuestionMultipleChoiceEditForm from './question-type-save-forms/question-multiple-choice.edit-form';
import QuestionSingleChoiceEditForm from './question-type-save-forms/question-single-choice.edit-form';
import { useGetTestQuestionQuery } from '../../api/test-questions.api';
import { NavigateToError, NavigateToNotFound } from '../../common/navigate';
import { TestQuestionType } from '../../models/test-question.interface';

interface Props {
  questionId: string;
}

export default function TestQuestionBodyEditForm({ questionId }: Props) {
  const { t } = useTranslation();

  const { data, isLoading, isFetching, error } = useGetTestQuestionQuery({
    id: questionId,
  });

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
      {(() => {
        switch (data.type) {
          case TestQuestionType.Input:
            return (
              <QuestionInputEditForm
                questionId={questionId}
                initialValues={data}
              />
            );
          case TestQuestionType.SingleChoice:
            return (
              <QuestionSingleChoiceEditForm
                questionId={questionId}
                initialValues={data}
              />
            );
          case TestQuestionType.MultipleChoice:
            return (
              <QuestionMultipleChoiceEditForm
                questionId={questionId}
                initialValues={data}
              />
            );
          case TestQuestionType.Matching:
            return (
              <QuestionMatchingEditForm
                questionId={questionId}
                initialValues={data}
              />
            );
          default:
            return null;
        }
      })()}
    </Spin>
  );
}
