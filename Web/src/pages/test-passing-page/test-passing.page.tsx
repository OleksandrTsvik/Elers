import { green } from '@ant-design/colors';
import { Radio, Skeleton, Statistic } from 'antd';
import dayjs from 'dayjs';
import { useState } from 'react';
import { useTranslation } from 'react-i18next';
import { useParams } from 'react-router-dom';

import TestPassingBreadcrumb from './test-passing.breadcrumb';
import TestQuestion from './test.question';
import { useGetTestSessionQuery } from '../../api/tests.api';
import { NavigateToError, NavigateToNotFound } from '../../common/navigate';
import { isNumber } from '../../utils/helpers';

export default function TestPassingPage() {
  const { testSessionId } = useParams();
  const { t } = useTranslation();

  const [questionId, setQuestionId] = useState<string>();

  const { data, isLoading, error } = useGetTestSessionQuery({
    id: testSessionId,
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
    <>
      <TestPassingBreadcrumb testId={data.testId} />

      {isNumber(data.timeLimitInMinutes) && (
        <Statistic.Countdown
          className="text-right"
          title={t('test_passing_page.time_left')}
          value={dayjs(data.startedAt)
            .add(data.timeLimitInMinutes, 'minute')
            .toString()}
        />
      )}

      <Radio.Group
        className="square-radio mt-field"
        buttonStyle="solid"
        value={questionId ?? data.questions[0].questionId}
        onChange={({ target }) => setQuestionId(target.value as string)}
      >
        {data.questions.map(({ questionId, isAnswered }, index) => (
          <Radio.Button
            key={questionId}
            value={questionId}
            style={{ backgroundColor: isAnswered ? green[6] : undefined }}
          >
            {index + 1}
          </Radio.Button>
        ))}
      </Radio.Group>

      <TestQuestion
        testSessionId={data.testSessionId}
        questionId={questionId ?? data.questions[0].questionId}
      />
    </>
  );
}
