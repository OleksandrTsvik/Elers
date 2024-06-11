import { Skeleton, Typography } from 'antd';
import { useTranslation } from 'react-i18next';
import { useParams } from 'react-router-dom';

import ResultsPreviousAttemptsTable from './results-previous-attempts.table';
import StartTestButton from './start-test.button';
import StartTestGuard from './start-test.guard';
import TestBreadcrumb from './test.breadcrumb';
import TestHead from './test.head';
import { useGetTestQuery } from '../../api/tests.api';
import { NavigateToError, NavigateToNotFound } from '../../common/navigate';
import { TextEditorOutput } from '../../common/typography';
import { DeadlineParagraph } from '../../shared';
import { isNumber } from '../../utils/helpers';

export default function TestPage() {
  const { courseId, id } = useParams();
  const { t } = useTranslation();

  const { data, isFetching, error } = useGetTestQuery({ id });

  if (isFetching) {
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
      <TestHead title={data.title} />
      <TestBreadcrumb courseId={courseId} title={data.title} />

      <Typography.Title level={3}>{data.title}</Typography.Title>

      {data.description && <TextEditorOutput text={data.description} />}

      <Typography.Paragraph>
        {t('course_test.number_attempts')}: {data.numberAttempts}
      </Typography.Paragraph>

      {isNumber(data.timeLimitInMinutes) && (
        <Typography.Paragraph>
          {t('course_test.time_limit')}: {data.timeLimitInMinutes}
        </Typography.Paragraph>
      )}

      <DeadlineParagraph
        deadline={data.deadline}
        title={t('course_test.deadline')}
        noDeadlineText={t('course_test.no_deadline')}
      />

      <Typography.Paragraph>
        {t('course_test.grading_method')}:{' '}
        {t([
          `course_test.grading_method_type.${data.gradingMethod}`,
          data.gradingMethod,
        ])}
      </Typography.Paragraph>

      {data.attempts.length > 0 && (
        <>
          <Typography.Title level={4}>
            {t('course_test.results_previous_attempts')}
          </Typography.Title>

          <ResultsPreviousAttemptsTable
            courseId={courseId}
            attemps={data.attempts}
          />
        </>
      )}

      <StartTestGuard
        courseId={courseId}
        attempts={data.attempts}
        numberAttempts={data.numberAttempts}
      >
        <StartTestButton courseId={courseId} testId={data.testId} />
      </StartTestGuard>
    </>
  );
}
