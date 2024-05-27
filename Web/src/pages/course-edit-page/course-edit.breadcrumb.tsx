import { useTranslation } from 'react-i18next';

import { CourseBreadcrumb } from '../../shared';

interface Props {
  courseId: string;
  title: string;
}

export default function CourseEditBreadcrumb({ courseId, title }: Props) {
  const { t } = useTranslation();

  return (
    <CourseBreadcrumb
      courseId={courseId}
      courseTitle={title}
      title={t('course_edit_page.title')}
    />
  );
}
