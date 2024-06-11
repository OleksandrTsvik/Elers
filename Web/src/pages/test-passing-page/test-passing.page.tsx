import { green } from '@ant-design/colors';
import { Radio, Skeleton } from 'antd';
import { useState } from 'react';
import { useParams } from 'react-router-dom';

import FinishTestButton from './finish-test.button';
import TestPassingBreadcrumb from './test-passing.breadcrumb';
import TestQuestion from './test.question';
import TimeLeftCountdown from './time-left.countdown';
import { useGetTestSessionQuery } from '../../api/tests.api';
import { NavigateToError, NavigateToNotFound } from '../../common/navigate';

export default function TestPassingPage() {
  const { testSessionId } = useParams();

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

      <TimeLeftCountdown
        startedAt={data.startedAt}
        timeLimitInMinutes={data.timeLimitInMinutes}
        testSessionId={data.testSessionId}
        testId={data.testId}
      />

      <FinishTestButton
        testSessionId={data.testSessionId}
        testId={data.testId}
      />

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
