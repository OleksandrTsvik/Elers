import { Empty, Skeleton, Spin, Typography } from 'antd';
import { useTranslation } from 'react-i18next';

import TestQuestionForm from './test-question.form';
import { useGetTestSessionQuestionQuery } from '../../api/tests.api';
import { ErrorAlert } from '../../common/error';
import { TextEditorOutput } from '../../common/typography';

interface Props {
  testSessionId: string;
  questionId: string;
}

export default function TestQuestion({ testSessionId, questionId }: Props) {
  const { t } = useTranslation();

  const { data, isLoading, isFetching, error } = useGetTestSessionQuestionQuery(
    { testSessionId, questionId },
  );

  if (isLoading) {
    return <Skeleton active />;
  }

  if (error) {
    return <ErrorAlert className="mt-field" error={error} />;
  }

  if (!data) {
    return (
      <Empty
        image={Empty.PRESENTED_IMAGE_SIMPLE}
        description={t('test_passing_page.failed_get_question')}
      />
    );
  }

  return (
    <Spin spinning={isFetching}>
      <Typography.Paragraph className="mt-field">
        {t('test_passing_page.points')}: {data.points}
      </Typography.Paragraph>

      <TextEditorOutput text={data.questionText} />

      <TestQuestionForm testSessionQuestion={data} />
    </Spin>
  );
}
