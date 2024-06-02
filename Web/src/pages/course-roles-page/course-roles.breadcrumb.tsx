import { useTranslation } from 'react-i18next';

import { CourseBreadcrumb } from '../../shared';

interface Props {
  courseId?: string;
}

export default function CourseRolesBreadcrumb({ courseId }: Props) {
  const { t } = useTranslation();

  return (
    <CourseBreadcrumb
      courseId={courseId}
      title={t('course_roles_page.title')}
    />
  );
}
