import { Skeleton } from 'antd';
import { useTranslation } from 'react-i18next';
import { Link, useParams } from 'react-router-dom';

import TestPassingHead from './test-passing.head';
import { useGetTestQuery } from '../../api/tests.api';
import { NavigateToError, NavigateToNotFound } from '../../common/navigate';
import { CourseBreadcrumb } from '../../shared';

interface Props {
  testId: string;
}

export default function TestPassingBreadcrumb({ testId }: Props) {
  const { courseId } = useParams();
  const { t } = useTranslation();

  const { data, isFetching, error } = useGetTestQuery({ id: testId });

  if (isFetching) {
    return (
      <Skeleton active title={false} paragraph={{ rows: 1, width: '100%' }} />
    );
  }

  if (error) {
    return <NavigateToError error={error} />;
  }

  if (!data) {
    return <NavigateToNotFound />;
  }

  return (
    <>
      <TestPassingHead title={data.title} />
      <CourseBreadcrumb
        courseId={courseId}
        items={[
          {
            title: (
              <Link to={`/courses/${courseId}/test/${data.testId}`}>
                {data.title}
              </Link>
            ),
          },
          {
            title: t('test_passing_page.title'),
          },
        ]}
      />
    </>
  );
}
