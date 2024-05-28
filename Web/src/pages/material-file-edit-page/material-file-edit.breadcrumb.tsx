import { useTranslation } from 'react-i18next';

import { CourseMaterialBreadcrumb } from '../../shared';

interface Props {
  courseId: string;
  title: string;
}

export default function MaterialFileEditBreadcrumb({ courseId, title }: Props) {
  const { t } = useTranslation();

  return (
    <CourseMaterialBreadcrumb
      courseId={courseId}
      courseTitle={title}
      title={t('material_file_edit_page.title')}
    />
  );
}
