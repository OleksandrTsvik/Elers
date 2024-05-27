import { useTranslation } from 'react-i18next';

import { CourseBreadcrumb } from '../../shared';

interface Props {
  courseId: string;
  title: string;
}

export default function CourseChangeImageBreadcrumb({
  courseId,
  title,
}: Props) {
  const { t } = useTranslation();

  return (
    <CourseBreadcrumb
      courseId={courseId}
      courseTitle={title}
      title={t('course_change_image_page.title')}
    />
  );
}
