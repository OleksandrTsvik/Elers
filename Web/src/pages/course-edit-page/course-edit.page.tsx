import { Skeleton, Spin } from 'antd';
import { useTranslation } from 'react-i18next';
import { useParams } from 'react-router-dom';

import CourseEditPageContent from './course-edit.page-content';
import { useGetCourseByIdQuery } from '../../api/courses.api';
import { NavigateToNotFound } from '../../shared';

export default function CourseEditPage() {
  const { courseId } = useParams();
  const { t } = useTranslation();

  const { data, isLoading, isFetching } = useGetCourseByIdQuery({
    id: courseId,
  });

  if (isLoading) {
    return <Skeleton />;
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
