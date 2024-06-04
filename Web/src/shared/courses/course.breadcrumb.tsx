import { Breadcrumb, Skeleton } from 'antd';
import { useTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';

import { useGetCourseByIdQuery } from '../../api/courses.api';
import { NavigateToError, NavigateToNotFound } from '../../common/navigate';

interface Props {
  courseId?: string;
  title: React.ReactNode;
}

export function CourseBreadcrumb({ courseId, title }: Props) {
  const { t } = useTranslation();

  const { data, isFetching, error } = useGetCourseByIdQuery({ id: courseId });

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
    <Breadcrumb
      className="mb-breadcrumb"
      items={[
        {
          title: <Link to="/">{t('course.courses')}</Link>,
        },
        {
          title: <Link to={`/courses/${data.id}`}>{data.title}</Link>,
        },
        {
          title,
        },
      ]}
    />
  );
}
