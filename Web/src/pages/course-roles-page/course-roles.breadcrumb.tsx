import { Skeleton } from 'antd';
import { useTranslation } from 'react-i18next';

import { CourseBreadcrumb } from '../../shared';

interface Props {
  isLoading: boolean;
  courseId?: string;
  courseTitle?: string;
}

export default function CourseRolesBreadcrumb({
  isLoading,
  courseId,
  courseTitle,
}: Props) {
  const { t } = useTranslation();

  if (isLoading) {
    return (
      <Skeleton active title={false} paragraph={{ rows: 1, width: '100%' }} />
    );
  }

  return (
    <CourseBreadcrumb
      courseId={courseId ?? ''}
      courseTitle={courseTitle}
      title={t('course_roles_page.title')}
    />
  );
}
