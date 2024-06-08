import { useTranslation } from 'react-i18next';

import { CourseMaterialBreadcrumb } from '../../shared';

interface Props {
  courseId: string;
  courseTitle: string;
}

export default function MaterialTestEditBreadcrumb({
  courseId,
  courseTitle,
}: Props) {
  const { t } = useTranslation();

  return (
    <CourseMaterialBreadcrumb
      courseId={courseId}
      courseTitle={courseTitle}
      title={t('material_test_edit_page.head_title')}
    />
  );
}
