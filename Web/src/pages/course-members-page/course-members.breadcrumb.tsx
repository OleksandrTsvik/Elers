import { useTranslation } from 'react-i18next';

import { CourseBreadcrumb } from '../../shared';

interface Props {
  courseId?: string;
}

export default function CourseMembersBreadcrumb({ courseId }: Props) {
  const { t } = useTranslation();

  return (
    <CourseBreadcrumb
      courseId={courseId}
      title={t('course_members_page.title')}
    />
  );
}
