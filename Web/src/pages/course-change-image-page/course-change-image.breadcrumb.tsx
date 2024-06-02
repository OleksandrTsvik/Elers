import { useTranslation } from 'react-i18next';

import { CourseBreadcrumb } from '../../shared';

interface Props {
  courseId: string;
}

export default function CourseChangeImageBreadcrumb({ courseId }: Props) {
  const { t } = useTranslation();

  return (
    <CourseBreadcrumb
      courseId={courseId}
      title={t('course_change_image_page.title')}
    />
  );
}
