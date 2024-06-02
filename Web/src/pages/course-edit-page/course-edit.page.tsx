import { Skeleton, Spin } from 'antd';
import { useTranslation } from 'react-i18next';
import { useParams } from 'react-router-dom';

import CourseEditPageContent from './course-edit.page-content';
import { useGetCourseByIdToEditQuery } from '../../api/courses.api';
import { NavigateToError, NavigateToNotFound } from '../../common/navigate';

export default function CourseEditPage() {
  const { courseId } = useParams();
  const { t } = useTranslation();

  const { data, isLoading, isFetching, error } = useGetCourseByIdToEditQuery({
    id: courseId,
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
    <Spin spinning={isFetching} tip={t('loading.changes')}>
      <CourseEditPageContent course={data} />
    </Spin>
  );
}
