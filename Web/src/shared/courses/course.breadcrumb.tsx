import { Breadcrumb, Skeleton } from 'antd';
import { ItemType } from 'antd/es/breadcrumb/Breadcrumb';
import { useTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';

import { useGetCourseByIdQuery } from '../../api/courses.api';
import { NavigateToError, NavigateToNotFound } from '../../common/navigate';

interface BaseProps {
  courseId: string | undefined;
}

interface TitleProps extends BaseProps {
  title: React.ReactNode;
  items?: never;
}

interface ItemsProps extends BaseProps {
  title?: never;
  items: ItemType[];
}

type Props = TitleProps | ItemsProps;

export function CourseBreadcrumb({ courseId, title, items }: Props) {
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
        ...(items ? items : [{ title }]),
      ]}
    />
  );
}
